using System.Collections.Generic;
using Buddy.Domain.Entities;
using Core.Domain;

namespace Buddy.Domain.Factories
{
    public class RegionFactory: IAggregateFactory<Region>
    {
        public Region Create(IList<IEvent> events)
        {
            var region = new Region(events);
            return region;
        }
    }
}
