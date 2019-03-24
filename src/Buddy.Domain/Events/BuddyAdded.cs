using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BuddyAdded: IEvent
    {
        public BuddyAdded(string id, string buddyId, bool isMerge)
        {
            Id = id;
            BuddyId = buddyId;
            IsMerge = isMerge;
        }

        public string Id { get; }
        public string BuddyId { get; }
        public bool IsMerge { get; }
    }
}
