using MediatR;

namespace Buddy.Application.CommandService.Group.Match
{
    public class MatchGroupCommand: INotification
    {
        public MatchGroupCommand(string groupId)
        {
            GroupId = groupId;
        }
        public string GroupId { get; }
    }
}
