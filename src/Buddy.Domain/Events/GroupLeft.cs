using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class GroupLeft: IEvent
    {
        public GroupLeft(string id, string groupId)
        {
            Id = id;
            GroupId = groupId;
        }
        public string Id { get; }
        public string GroupId { get; }
    }
}
