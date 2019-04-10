using Buddy.Domain.Enums;
using Core.Domain;
using Core.Domain.Events;

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
