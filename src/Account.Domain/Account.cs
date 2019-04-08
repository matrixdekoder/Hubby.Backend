using System;
using Core.Domain;

namespace Account.Domain
{
    public class Account: Aggregate<Account>
    {
        public void Register(string id, string password)
        {
            if(Version > 0)
                throw new InvalidOperationException($"Account with username {id} already exists.");

            var e = new AccountRegistered(id, password);
            Publish(e);
        }

        private void When(AccountRegistered e)
        {
            Id = e.Id;
        }
    }
}
