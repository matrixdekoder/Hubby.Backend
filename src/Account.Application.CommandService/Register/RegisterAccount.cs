using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccount : IRequest<RegisterAccountResponse>
    {
        public RegisterAccount(string id, string password)
        {
            Id = id;
            Password = password;
        }

        public string Id { get; }
        public string Password { get; }
    }
}
