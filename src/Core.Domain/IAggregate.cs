using System.Collections.Generic;

namespace Core.Domain
{
    public interface IAggregate: IEntity
    {
        IEnumerable<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
