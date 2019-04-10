using Core.Domain;
using Core.Domain.Events;

namespace Region.Domain
{
    public class GroupAddedToRegion: IEvent
    {
        public GroupAddedToRegion(string id, string groupId)
        {
            Id = id;
            GroupId = groupId;
        }

        public string Id { get; }
        public string GroupId { get; }
    }
}
