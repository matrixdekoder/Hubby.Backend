using Core.Domain;

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
