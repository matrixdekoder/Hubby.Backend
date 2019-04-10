using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Core.Domain.Events;

namespace Library.EventStore.Persistence
{
    public class EventTransaction
    {
        private readonly ConcurrentDictionary<string, List<IEvent>> _aggregateEvents;

        public EventTransaction(long id)
        {
            Id = id;
            _aggregateEvents = new ConcurrentDictionary<string, List<IEvent>>();

        }

        public long Id { get; }
        
        public void AddEvents(string stream, IList<IEvent> events)
        {
            _aggregateEvents.AddOrUpdate(stream, events.ToList(), (aggregateId, aggregateEvents) =>
            {
                aggregateEvents.AddRange(events);
                return aggregateEvents;
            });
        }

        public IList<IEvent> GetEvents(string stream)
        {
            _aggregateEvents.TryGetValue(stream, out var events);
            return events ?? new List<IEvent>();
        }
    }
}
