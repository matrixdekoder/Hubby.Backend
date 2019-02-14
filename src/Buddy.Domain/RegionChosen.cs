using Core.Domain;

namespace Buddy.Domain
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
