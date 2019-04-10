using Core.Domain;
using Core.Domain.Events;

namespace Account.Domain
{
    public class AccountRegistered: IEvent
    {
        public AccountRegistered(string id, string password, string buddyId)
        {
            Id = id;
            Password = password;
            BuddyId = buddyId;
        }

        public string Id { get; set; }
        public string Password { get; }
        public string BuddyId { get; }
    }
}
