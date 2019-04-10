using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class BuddyCreated: IEvent
    {
        public BuddyCreated(string accountId, string id)
        {
            AccountId = accountId;
            Id = id;
        }

        public string AccountId { get; }
        public string Id { get; set; }
    }
}
