using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccount : IRequest<RegisterAccountResponse>
    {
        public RegisterAccount(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}
