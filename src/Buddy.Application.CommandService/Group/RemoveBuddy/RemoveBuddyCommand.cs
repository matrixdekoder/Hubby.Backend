using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddy
{
    public class RemoveBuddyCommand: INotification
    {
        public RemoveBuddyCommand(string buddyId, string groupId, long transactionId)
        {
            BuddyId = buddyId;
            GroupId = groupId;
            TransactionId = transactionId;
        }

        public string BuddyId { get; }
        public string GroupId { get; }
        public long TransactionId { get; }
    }
}
