using System;
using Core.Domain;

namespace Account.Domain
{
    public class AccountRegistered: IEvent
    {
        public AccountRegistered(Guid id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }

        public Guid Id { get; }
        public string Username { get; }
        public string Password { get; }
    }
}
