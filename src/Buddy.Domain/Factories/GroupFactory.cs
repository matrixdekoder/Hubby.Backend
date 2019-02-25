using System;
using System.Collections.Generic;
using Buddy.Domain.Entities;
using Core.Domain;

namespace Buddy.Domain.Factories
{
    public class GroupFactory: IAggregateFactory<Group>
    {
        public Group Create(IList<IEvent> events)
        {
            var group = new Group(events);
            return group;
        }
    }
}
