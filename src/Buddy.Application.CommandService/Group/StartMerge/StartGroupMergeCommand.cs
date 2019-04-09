using MediatR;

namespace Buddy.Application.CommandService.Group.StartMerge
{
    public class StartGroupMergeCommand: INotification
    {
        public StartGroupMergeCommand(string groupId, string matchedGroupId, long transactionId)
        {
            GroupId = groupId;
            MatchedGroupId = matchedGroupId;
            TransactionId = transactionId;
        }

        public string GroupId { get; }
        public string MatchedGroupId { get; }
        public long TransactionId { get; }
    }
}
