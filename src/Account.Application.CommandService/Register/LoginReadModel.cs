using System;
using Core.Application;

namespace Account.Application.CommandService.Register
{
    public class LoginReadModel: IReadModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
