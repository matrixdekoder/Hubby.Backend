using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BuddyAdded: IEvent
    {
        public BuddyAdded(string id, string buddyId)
        {
            Id = id;
            BuddyId = buddyId;
        }

        public string Id { get; }
        public string BuddyId { get; }
    }
}
