using MediatR;

namespace Buddy.Application.CommandService.Group.Match
{
    public class MatchGroupCommand: INotification
    {
        public MatchGroupCommand(string groupId, long transactionId)
        {
            GroupId = groupId;
            TransactionId = transactionId;
        }
        public string GroupId { get; }
        public long TransactionId { get; }
    }
}
