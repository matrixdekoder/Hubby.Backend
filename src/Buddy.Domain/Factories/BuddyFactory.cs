using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Factories
{
    public class BuddyFactory: IAggregateFactory<Entities.Buddy>
    {
        public Entities.Buddy Create(IList<IEvent> events)
        {
            return new Entities.Buddy(events);
        }
    }
}
