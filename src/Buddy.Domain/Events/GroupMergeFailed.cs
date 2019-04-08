using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupMergeFailed: IEvent
    {
        public GroupMergeFailed(string id, string otherGroupId)
        {
            Id = id;
            OtherGroupId = otherGroupId;
        }

        public string Id { get; }
        public string OtherGroupId { get; }
    }
}
