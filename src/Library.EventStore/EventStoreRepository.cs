using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using EventStore.ClientAPI;

namespace Library.EventStore
{
    public class EventStoreRepository<T> : IEventStoreRepository<T> where T : IAggregate
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly IAggregateFactory<T> _aggregateFactory;

        public EventStoreRepository(IEventStoreContext eventStoreContext, IAggregateFactory<T> aggregateFactory)
        {
            _eventStoreContext = eventStoreContext;
            _aggregateFactory = aggregateFactory;
        }

        public async Task<T> GetById(Guid id)
        {
            var events = new List<IEvent>();
            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;
            var streamName = GetStreamName(id);

            do
            {
                currentSlice = await _eventStoreContext.Connection.ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false);
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(x => x.DeserializeEvent()));
            } while (!currentSlice.IsEndOfStream);

            return _aggregateFactory.Create(events);
        }

        public async Task Save(T aggregate)
        {
            var events = aggregate.GetUncommittedEvents().ToArray();
            if (!events.Any()) return;

            var streamName = GetStreamName(aggregate.GetType(), aggregate.Id);
            var eventsToSave = events.Select(x => x.ToEventData()).ToList();
            await _eventStoreContext.Connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventsToSave);
            aggregate.ClearUncommittedEvents();
        }

        private string GetStreamName(Guid id)
        {
            return GetStreamName(typeof(T), id);
        }

        private static string GetStreamName(Type type, Guid id)
        {
            return $"{type.Name}-{id}";
        }
    }
}
