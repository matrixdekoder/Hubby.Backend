using System.Collections.Generic;
using Core.Domain;

namespace Region.Domain
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
