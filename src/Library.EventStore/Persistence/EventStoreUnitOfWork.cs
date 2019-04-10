using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Infrastructure;
using Core.Domain.Events;
using EventStore.ClientAPI;

namespace Library.EventStore.Persistence
{
    public class EventStoreUnitOfWork: IUnitOfWork
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly ConcurrentDictionary<string, long> _transactions;
        private readonly ConcurrentDictionary<string, List<IEvent>> _uncommittedEvents;

        public EventStoreUnitOfWork(IEventStoreContext eventStoreContext)
        {
            _eventStoreContext = eventStoreContext;
            _transactions = new ConcurrentDictionary<string, long>();
            _uncommittedEvents = new ConcurrentDictionary<string, List<IEvent>>();
        }

        public async Task Commit()
        {
            foreach (var transactionId in _transactions.Values)
            {
                var transaction = _eventStoreContext.Connection.ContinueTransaction(transactionId);
                await transaction.CommitAsync();
                transaction.Dispose();
            }
        }

        public void Rollback()
        {
            foreach (var transactionId in _transactions.Values)
            {
                var transaction = _eventStoreContext.Connection.ContinueTransaction(transactionId);
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        public async Task<EventStoreTransaction> GetTransaction(string id, string stream)
        {
            var transaction = _transactions.TryGetValue(id, out var transactionId)
                ? _eventStoreContext.Connection.ContinueTransaction(transactionId)
                : await _eventStoreContext.Connection.StartTransactionAsync(stream, ExpectedVersion.Any);

            _transactions.AddOrUpdate(id, transaction.TransactionId, (s, l) => transaction.TransactionId);

            return transaction;
        }

        public void WriteEvents(string id, IList<IEvent> events)
        {
            _uncommittedEvents.AddOrUpdate(id, new List<IEvent>(events), (s, list) =>
            {
                list.AddRange(events);
                return list;
            });
        }

        public IList<IEvent> GetUncommittedEvents(string id)
        {
            _uncommittedEvents.TryGetValue(id, out var uncommittedEvents);
            return uncommittedEvents ?? new List<IEvent>();
        }
    }
}
