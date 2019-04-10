using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Infrastructure;
using Core.Application;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Events;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;
using MediatR;

namespace Library.EventStore.Persistence
{
    public class EventStoreRepository : IRepository
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        

        public EventStoreRepository(IEventStoreContext eventStoreContext, IUnitOfWork unitOfWork, IMediator mediator)
        {
            _eventStoreContext = eventStoreContext;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            
        }

        public async Task<T> GetById<T>(string id) where T : IAggregate, new()
        {
            // Retrieve events from store
            var events = new List<IEvent>();
            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;
            var streamName = GetStreamName<T>(id);

            do
            {
                currentSlice = await _eventStoreContext.Connection.ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false);
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
            } while (!currentSlice.IsEndOfStream);

            // Retrieve events from unit of work
            var transactionEvents = _unitOfWork.GetUncommittedEvents(id);
            events.AddRange(transactionEvents);

            // Rebuild aggregate
            var aggregate = new T();
            aggregate.Rehydrate(events);
            return aggregate;
        }

        public async Task Save<T>(T aggregate) where T : IAggregate
        {
            var events = aggregate.GetUncommittedEvents().ToArray();
            if (!events.Any()) return;

            // Add events to transaction
            await Save(aggregate, events);

            // Dispatch events for command listeners
            foreach (var @event in events)
            {
                var commandEvent = GenericEventActivator.CreateCommandEvent(@event);
                await _mediator.Publish(commandEvent);
            }

            aggregate.ClearUncommittedEvents();
        }

        private async Task Save<T>(T aggregate, IList<IEvent> events) where T : IAggregate
        {
            // Save as transaction in EventStore
            var streamName = GetStreamName(aggregate.GetType(), aggregate.Id);
            var transaction = await _unitOfWork.GetTransaction(aggregate.Id, streamName);
            var eventsToSave = events.Select(x => x.ToEventData()).ToList();
            await transaction.WriteAsync(eventsToSave);

            // Temporary save events in unit of work for a scoped lifetime,
            // as they are only persisted on the end of the request.
            _unitOfWork.WriteEvents(aggregate.Id, events);
        }

        private string GetStreamName<T>(string id)
        {
            return GetStreamName(typeof(T), id);
        }

        private static string GetStreamName(Type type, string id)
        {
            return $"{type.Name}-{id}";
        }
    }
}
