using System;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountResponse
    {
        public Guid Id { get; }
        public string Username { get; }

        public RegisterAccountResponse(Guid id, string username)
        {
            Id = id;
            Username = username;
        }
    }
}
