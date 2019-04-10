using Buddy.Domain.Enums;
using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class BuddyAdded: IEvent
    {
        public BuddyAdded(string id, string buddyId, BuddyJoinType type)
        {
            Id = id;
            BuddyId = buddyId;
            Type = type;
        }

        public string Id { get; }
        public string BuddyId { get; }
        public BuddyJoinType Type { get; }
    }
}
