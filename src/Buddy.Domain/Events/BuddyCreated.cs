using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BuddyCreated: IEvent
    {
        public BuddyCreated(string id, string accountId)
        {
            Id = id;
            AccountId = accountId;
        }

        public string Id { get; set; }
        public string AccountId { get; }
    }
}
