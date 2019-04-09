using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;

namespace Library.EventStore.Persistence
{
    public class EventStoreRepository : IRepository
    {
        private readonly IEventStoreContext _eventStoreContext;

        public EventStoreRepository(IEventStoreContext eventStoreContext)
        {
            _eventStoreContext = eventStoreContext;
        }

        public async Task<T> GetById<T>(string id) where T : IAggregate, new()
        {
            var events = new List<IEvent>();
            StreamEventsSlice currentSlice;
            long nextSliceStart = StreamPosition.Start;
            var streamName = GetStreamName<T>(id);

            do
            {
                currentSlice = await _eventStoreContext.Connection.ReadStreamEventsForwardAsync(streamName, nextSliceStart, 200, false);
                nextSliceStart = currentSlice.NextEventNumber;
                events.AddRange(currentSlice.Events.Select(x => EventStoreExtensions.DeserializeEvent(x)));
            } while (!currentSlice.IsEndOfStream);

            var aggregate = new T();
            aggregate.Rehydrate(events);
            return aggregate;
        }

        public async Task Save<T>(T aggregate) where T : IAggregate
        {
            var events = aggregate.GetUncommittedEvents().ToArray();
            if (!events.Any()) return;

            var streamName = GetStreamName(aggregate.GetType(), aggregate.Id);
            var eventsToSave = events.Select(x => x.ToEventData()).ToList();
            await _eventStoreContext.Connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventsToSave);
            aggregate.ClearUncommittedEvents();
        }

        protected string GetStreamName<T>(string id)
        {
            return GetStreamName(typeof(T), id);
        }

        protected static string GetStreamName(Type type, string id)
        {
            return $"{type.Name}-{id}";
        }
    }
}
