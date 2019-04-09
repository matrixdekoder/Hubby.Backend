using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Core.Application.Saga
{
    public class SagaOrchestrator : ISagaOrchestrator
    {
        private readonly IMediator _mediator;
        private readonly IList<long> _transactions;
        private readonly ISagaRepository _repository;

        public SagaOrchestrator(IMediator mediator, ISagaRepository repository)
        {
            _mediator = mediator;
            _repository = repository;
            _transactions = new List<long>();
            TransactionEvents = new Dictionary<string, IList<IEvent>>();
        }

        public IDictionary<string, IList<IEvent>> TransactionEvents { get; }

        public async Task<long> StartTransaction<T>(string id) where T : IAggregate
        {
            var transactionId = await _repository.StartTransaction<T>(id);
            _transactions.Add(transactionId);
            return transactionId;
        }

        public async Task PublishCommand(INotification notification, CancellationToken token)
        {
            try
            {
                await _mediator.Publish(notification, token);
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }
        }

        public void AddEventToTransaction(IEvent @event)
        {
            TransactionEvents.TryGetValue(@event.Id, out var events);

            if (events == null)
                TransactionEvents.Add(@event.Id, new List<IEvent> { @event });
            else
                events.Add(@event);
        }

        public IList<IEvent> GetTransactionEvents(string id)
        {
            TransactionEvents.TryGetValue(id, out var events);
            return events ?? new List<IEvent>();
        }

        public async Task Commit()
        {
            try
            {
                foreach (var transaction in _transactions)
                {
                    await _repository.Commit(transaction);
                }
            }
            catch (Exception)
            {
                RollBack();
                throw;
            }
        }

        private void RollBack()
        {
            foreach (var transaction in _transactions)
            {
                _repository.Rollback(transaction);
            }
        }
    }
}
