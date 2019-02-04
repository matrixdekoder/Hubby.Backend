using System;
using System.Collections.Generic;
using Core.Domain;

namespace Account.Domain
{
    public class Account: Aggregate
    {
        private string _password;

        public Account(IEnumerable<IEvent> events): base(events)
        {
        }

        public string Username { get; private set; }

        public void Register(Guid id, string username, string password)
        {
            if(Version > 0)
                throw new InvalidOperationException("Can't register an account twice.");

            Id = id;
            Username = username;
            _password = password;

            var e = new AccountRegistered(Id, username, password);

            Publish(e);
        }

        private void When(AccountRegistered e)
        {
            Id = e.Id;
            Username = e.Username;
            _password = e.Password;
        }
    }
}
