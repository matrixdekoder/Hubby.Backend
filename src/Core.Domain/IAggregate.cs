using System.Collections.Generic;

namespace Core.Domain
{
    public interface IAggregate: IEntity
    {
        void Rehydrate(IEnumerable<IEvent> events);
        IEnumerable<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
