using Core.Domain;
using MediatR;

namespace Core.Application
{
    public class QueryEvent<T>: INotification where T : IEvent
    {
        public QueryEvent(T e)
        {
            Event = e;
        }
        public T Event { get; }
    }
}
