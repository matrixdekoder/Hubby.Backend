using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class RegionChosen : IEvent
    {
        public RegionChosen(string id, string regionId)
        {
            Id = id;
            RegionId = regionId;
        }

        public string Id { get; }
        public string RegionId { get; }
    }
}
