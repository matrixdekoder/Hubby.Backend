using Buddy.Domain.Enums;
using MediatR;

namespace Buddy.Application.CommandService.Group.SetStatus
{
    public class SetGroupStatusCommand: INotification
    {
        public SetGroupStatusCommand(string groupId, GroupStatus status)
        {
            GroupId = groupId;
            Status = status;
        }

        public string GroupId { get; }
        public GroupStatus Status { get; }
    }
}
