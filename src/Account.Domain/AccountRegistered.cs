using Core.Domain;

namespace Account.Domain
{
    public class AccountRegistered: IEvent
    {
        public AccountRegistered(string id, string password)
        {
            Id = id;
            Password = password;
        }

        public string Id { get; set; }
        public string Password { get; }
    }
}
