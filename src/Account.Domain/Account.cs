using System;
using Core.Domain;
using Core.Domain.Entities;

namespace Account.Domain
{
    public class Account: Aggregate<Account>
    {
        public void Register(string id, string password)
        {
            if(Version > 0)
                throw new InvalidOperationException($"Account with username {id} already exists.");

            var buddyId = Guid.NewGuid().ToString();
            var e = new AccountRegistered(id, password, buddyId);
            Publish(e);
        }

        private void When(AccountRegistered e)
        {
            Id = e.Id;
        }
    }
}
