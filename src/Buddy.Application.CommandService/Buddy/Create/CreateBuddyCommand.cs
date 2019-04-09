using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class CreateBuddyCommand: INotification
    {
        public CreateBuddyCommand(string accountId, long transactionId)
        {
            AccountId = accountId;
            TransactionId = transactionId;
        }

        public string AccountId { get; }
        public long TransactionId { get; }
    }
}
