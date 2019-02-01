using MediatR;

namespace Core.Domain
{
    public interface IEvent: IEntity, INotification
    {
    }
}
