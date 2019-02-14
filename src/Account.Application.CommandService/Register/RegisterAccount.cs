using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccount : INotification
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
