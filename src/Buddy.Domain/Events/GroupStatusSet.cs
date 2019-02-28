using Buddy.Domain.Enums;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupStatusSet: IEvent
    {
        public GroupStatusSet(string id, GroupStatus status)
        {
            Id = id;
            Status = status;
        }

        public string Id { get; }
        public GroupStatus Status { get; }
    }
}
