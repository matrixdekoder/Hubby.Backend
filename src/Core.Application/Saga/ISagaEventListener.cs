using Core.Domain;
using MediatR;

namespace Core.Application.Saga
{
    public interface ISagaEventListener<T> : INotificationHandler<SagaEvent<T>> where T: IEvent
    {
    }
}
