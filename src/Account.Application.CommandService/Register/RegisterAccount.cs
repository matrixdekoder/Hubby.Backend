using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccount : IRequest<RegisterAccountResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
