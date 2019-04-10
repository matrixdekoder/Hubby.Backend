using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Events;
using Core.Infrastructure;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;

namespace Library.EventStore.Persistence
{
    public class EventStoreUnitOfWork: IUnitOfWork
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly ConcurrentDictionary<string, EventTransaction> _transactions;

        public EventStoreUnitOfWork(IEventStoreContext eventStoreContext)
        {
            _eventStoreContext = eventStoreContext;
            _transactions = new ConcurrentDictionary<string, EventTransaction>();
        }

        public IList<IEvent> GetEvents(string stream)
        {
            _transactions.TryGetValue(stream, out var eventTransaction);
            return eventTransaction?.GetEvents(stream) ?? new List<IEvent>();
        }

        public async Task SaveChanges(string stream, IList<IEvent> events)
        {
            var transaction = await GetTransaction(stream);
            var transactionEvents = events.Select(x => x.ToEventData()).ToList();
            await transaction.WriteAsync(transactionEvents);

            if (_transactions.TryGetValue(stream, out var eventTransaction))
            {
                eventTransaction.AddEvents(stream, events);
            }
        }

        public async Task Commit()
        {
            foreach (var eventTransaction in _transactions.Values)
            {
                var transaction = _eventStoreContext.Connection.ContinueTransaction(eventTransaction.Id);
                await transaction.CommitAsync();
                transaction.Dispose();
            }
        }

        public void Rollback()
        {
            foreach (var eventTransaction in _transactions.Values)
            {
                var transaction = _eventStoreContext.Connection.ContinueTransaction(eventTransaction.Id);
                transaction.Rollback();
                transaction.Dispose();
            }
        }

        private async Task<EventStoreTransaction> GetTransaction(string stream)
        {
            var transaction = _transactions.TryGetValue(stream, out var tx)
                ? _eventStoreContext.Connection.ContinueTransaction(tx.Id)
                : await CreateTransaction(stream);

            return transaction;
        }

        private async Task<EventStoreTransaction> CreateTransaction(string stream)
        {
            var transaction = await _eventStoreContext.Connection.StartTransactionAsync(stream, ExpectedVersion.Any);
            _transactions.TryAdd(stream, new EventTransaction(transaction.TransactionId));
            return transaction;
        }
    }
}
