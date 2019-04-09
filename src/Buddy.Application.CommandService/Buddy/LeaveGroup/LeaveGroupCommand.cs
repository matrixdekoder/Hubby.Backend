using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class LeaveGroupCommand: INotification
    {
        public LeaveGroupCommand(string buddyId, long transactionId)
        {
            BuddyId = buddyId;
            TransactionId = transactionId;
        }

        public string BuddyId { get; }
        public long TransactionId { get; }
    }
}
