using MediatR;

namespace Buddy.Application.CommandService.Group.Clear
{
    public class ClearGroupCommand: INotification
    {
        public ClearGroupCommand(string groupId)
        {
            GroupId = groupId;
        }

        public string GroupId { get; }
    }
}
