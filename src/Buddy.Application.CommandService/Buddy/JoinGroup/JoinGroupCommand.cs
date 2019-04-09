using Buddy.Domain.Enums;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class JoinGroupCommand: INotification
    {
        public JoinGroupCommand(string buddyId, string groupId, BuddyJoinType type, long transactionId)
        {
            BuddyId = buddyId;
            GroupId = groupId;
            Type = type;
            TransactionId = transactionId;
        }

        public string BuddyId { get; }
        public string GroupId { get; }
        public BuddyJoinType Type { get; }
        public long TransactionId { get; }
    }
}
