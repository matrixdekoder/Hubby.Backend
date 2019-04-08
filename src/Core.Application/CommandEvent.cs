using Core.Domain;
using MediatR;

namespace Core.Application
{
    public class CommandEvent<T>: INotification where T : IEvent
    {
        public CommandEvent(T e)
        {
            Event = e;
        }
        public T Event { get; }
    }
}
