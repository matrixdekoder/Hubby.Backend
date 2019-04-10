using Core.Domain;
using Core.Domain.Events;

namespace Region.Domain
{
    public class RegionCreated: IEvent
    {
        public RegionCreated(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; }
        public string Name { get; }
    }
}
