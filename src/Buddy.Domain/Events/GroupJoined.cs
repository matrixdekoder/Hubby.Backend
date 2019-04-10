using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class GroupJoined: IEvent
    {
        public GroupJoined(string id, string groupId)
        {
            Id = id;
            GroupId = groupId;
        }

        public string Id { get; }
        public string GroupId { get; }
    }
}
