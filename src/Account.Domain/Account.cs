using System;
using Core.Domain;

namespace Account.Domain
{
    public class Account: Aggregate<Account>
    {
        private string _buddyId;

        public void Register(string id, string password)
        {
            if(Version > 0)
                throw new InvalidOperationException($"Account with username {id} already exists.");

            var e = new AccountRegistered(id, password);
            Publish(e);
        }

        public void LinkBuddy(string buddyId)
        {
            if(!string.IsNullOrEmpty(_buddyId))
                throw new InvalidOperationException($"Account {Id} has already a Buddy linked.");

            var e = new BuddyLinked(Id, buddyId);
            Publish(e);
        }

        private void When(AccountRegistered e)
        {
            Id = e.Id;
        }

        private void When(BuddyLinked e)
        {
            _buddyId = e.BuddyId;
        }
    }
}
