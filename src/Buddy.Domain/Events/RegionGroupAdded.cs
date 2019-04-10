using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class RegionGroupAdded: IEvent
    {
        public RegionGroupAdded(string id, string regionGroupId)
        {
            Id = id;
            RegionGroupId = regionGroupId;
        }

        public string Id { get; }
        public string RegionGroupId { get; }
    }
}
