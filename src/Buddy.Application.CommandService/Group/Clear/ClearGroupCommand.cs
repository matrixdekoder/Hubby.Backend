using MediatR;

namespace Buddy.Application.CommandService.Group.Clear
{
    public class ClearGroupCommand: INotification
    {
        public ClearGroupCommand(string groupId, long transactionId)
        {
            GroupId = groupId;
            TransactionId = transactionId;
        }

        public string GroupId { get; }
        public long TransactionId { get; }
    }
}
