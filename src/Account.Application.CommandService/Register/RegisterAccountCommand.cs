using MediatR;

namespace Account.Application.CommandService.Register
{
    public class RegisterAccountCommand : INotification
    {
        public RegisterAccountCommand(string id, string password, long transactionId)
        {
            Id = id;
            Password = password;
            TransactionId = transactionId;
        }

        public string Id { get; }
        public string Password { get; }
        public long TransactionId { get; }
    }
}
