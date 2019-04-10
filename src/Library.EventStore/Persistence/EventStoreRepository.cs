using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application;
using Core.Domain.Entities;
using Core.Domain.Events;
using Core.Infrastructure;
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
            var events = await GetEvents<T>(id);
            var aggregate = new T();
            aggregate.Rehydrate(events);
            return aggregate;
        }

        public async Task Save<T>(T aggregate) where T : IAggregate
        {
            var events = aggregate.GetUncommittedEvents().ToArray();
            if (!events.Any()) return;

            var stream = GetStreamName<T>(aggregate.Id);
            await _unitOfWork.SaveChanges(stream, events);

            await PublishEvents(events);
            aggregate.ClearUncommittedEvents();
        }

        private async Task<IList<IEvent>> GetEvents<T>(string id)
        {
            var events = new List<IEvent>();
            var stream = GetStreamName<T>(id);

            var storeEvents = await GetStoreEvents(stream);
            var transactionEvents = _unitOfWork.GetEvents(stream);

            events.AddRange(storeEvents);
            events.AddRange(transactionEvents);

            return events;
        }

        private async Task<IList<IEvent>> GetStoreEvents(string stream)
        {
            var events = new List<IEvent>();
            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;

            do
            {
                currentSlice = await _eventStoreContext.Connection.ReadStreamEventsForwardAsync(stream, nextSliceStart, 200, false);
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
            } while (!currentSlice.IsEndOfStream);

            return events;
        }

        private async Task PublishEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                var commandEvent = GenericEventActivator.CreateCommandEvent(@event);
                await _mediator.Publish(commandEvent);
            }
        }

        private static string GetStreamName<T>(string id)
        {
            return $"{typeof(T).Name}-{id}";
        }
    }
}
