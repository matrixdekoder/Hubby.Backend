using Buddy.Domain.Enums;
using Core.Domain;

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
