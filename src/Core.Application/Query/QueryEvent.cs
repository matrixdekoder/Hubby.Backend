using Core.Domain;
using MediatR;

namespace Core.Application.Query
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
