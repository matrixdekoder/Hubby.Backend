using System;
using System.Collections.Generic;
using Core.Domain;

namespace Account.Domain
{
    public class Account: Aggregate<Account>
    {
        private string _password;
        private string _username;

        public Account(IEnumerable<IEvent> events): base(events)
        {
        }

        public void Register(string id, string username, string password)
        {
            if(Version > 0)
                throw new InvalidOperationException($"Account with username {username} already exists.");

            var e = new AccountRegistered(id, username, password);
            Publish(e);
        }

        private void When(AccountRegistered e)
        {
            Id = e.Id;
            _username = e.Username;
            _password = e.Password;
        }
    }
}
