using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain;

namespace Buddy.Domain
{
    public class BuddyFactory: IAggregateFactory<Buddy>
    {
        public Buddy Create(IList<IEvent> events)
        {
            return new Buddy(events);
        }
    }
}
