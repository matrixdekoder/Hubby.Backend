using Core.Domain;
using MediatR;

namespace Core.Application.Saga
{
    public class SagaEvent<T>: INotification where T : IEvent
    {
        public SagaEvent(T @event, long transactionId)
        {
            Event = @event;
            TransactionId = transactionId;
        }

        public T Event { get; }
        public long TransactionId { get; }
    }
}
