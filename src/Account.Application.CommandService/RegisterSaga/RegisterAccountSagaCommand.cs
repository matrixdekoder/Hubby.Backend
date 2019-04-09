using MediatR;

namespace Account.Application.CommandService.RegisterSaga
{
    public class RegisterAccountSagaCommand: INotification
    {
        public RegisterAccountSagaCommand(string id, string password)
        {
            Id = id;
            Password = password;
        }

        public string Id { get; }
        public string Password { get; }
    }
}
