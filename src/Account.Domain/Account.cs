using System;
using System.Collections.Generic;
using Core.Domain;

namespace Account.Domain
{
    public class Account: Aggregate<Account>
    {
        private string _password;

        public Account(IEnumerable<IEvent> events): base(events)
        {
        }

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
            _password = e.Password;
        }
    }
}
