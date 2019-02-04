using System;
using Core.Domain;

namespace Account.Domain
{
    public class Login: IEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
