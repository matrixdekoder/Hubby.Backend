using System;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountResponse
    {
        public RegisterAccountResponse(string id, string username)
        {
            Id = id;
            Username = username;
        }
        
        public string Id { get; }
        public string Username { get; }
    }
}
