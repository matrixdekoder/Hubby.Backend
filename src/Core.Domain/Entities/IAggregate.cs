using System.Collections.Generic;
using Core.Domain.Events;

namespace Core.Domain.Entities
{
    public interface IAggregate: IEntity
    {
        void Rehydrate(IEnumerable<IEvent> events);
        IEnumerable<IEvent> GetUncommittedEvents();
        void ClearUncommittedEvents();
    }
}
