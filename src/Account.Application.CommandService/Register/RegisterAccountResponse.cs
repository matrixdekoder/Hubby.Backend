using System;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountResponse
    {
        public RegisterAccountResponse(string id)
        {
            Id = id;
        }
        
        public string Id { get; }
    }
}
