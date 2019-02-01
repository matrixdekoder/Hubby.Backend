using System.Collections.Generic;

namespace Core.Domain
{
    public interface IAggregateFactory<out T> where T : IAggregate
    {
        T Create(IList<IEvent> events);
    }
}
