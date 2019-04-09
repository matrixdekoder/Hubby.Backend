using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Application;
using Core.Application.Saga;
using Core.Domain;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;
using MediatR;

namespace Library.EventStore.Persistence
{
    public class SagaRepository: EventStoreRepository, ISagaRepository
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly IMediator _mediator;

        public SagaRepository(IEventStoreContext eventStoreContext, IMediator mediator): base(eventStoreContext)
        {
            _eventStoreContext = eventStoreContext;
            _mediator = mediator;
        }

        public async Task<long> StartTransaction<T>(string aggregateId) where T : IAggregate
        {
            var stream = GetStreamName<T>(aggregateId);
            var transaction = await _eventStoreContext.Connection.StartTransactionAsync(stream, ExpectedVersion.Any);
            return transaction.TransactionId;
        }

        public async Task Save<T>(long id, T aggregate) where T : IAggregate
        {
            var transaction = _eventStoreContext.Connection.ContinueTransaction(id);
            var events = aggregate.GetUncommittedEvents().ToList();
            var eventDataList = events.Select(x => x.ToEventData());
            await transaction.WriteAsync(eventDataList);

            foreach (var @event in events)
            {
                var sagaEvent = CreateSagaEvent(@event, id);
                await _mediator.Publish(sagaEvent);
            }
            
            aggregate.ClearUncommittedEvents();
        }

        public async Task Commit(long id)
        {
            var transaction = _eventStoreContext.Connection.ContinueTransaction(id);
            await transaction.CommitAsync();
            transaction.Dispose();
        }

        public void Rollback(long id)
        {
            var transaction = _eventStoreContext.Connection.ContinueTransaction(id);
            transaction.Rollback();
            transaction.Dispose();
        }

        private static object CreateSagaEvent(IEvent @event, long transactionId)
        {
            var generic = typeof(SagaEvent<>);
            var type = @event.GetType();
            var genericType = generic.MakeGenericType(type);
            return Activator.CreateInstance(genericType, @event, transactionId);
        }
    }
}
