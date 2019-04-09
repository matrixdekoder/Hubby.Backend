using Buddy.Domain.Enums;
using MediatR;

namespace Buddy.Application.CommandService.Group.SetStatus
{
    public class SetGroupStatusCommand: INotification
    {
        public SetGroupStatusCommand(string groupId, GroupStatus status, long transactionId)
        {
            GroupId = groupId;
            Status = status;
            TransactionId = transactionId;
        }

        public string GroupId { get; }
        public GroupStatus Status { get; }
        public long TransactionId { get; }
    }
}
