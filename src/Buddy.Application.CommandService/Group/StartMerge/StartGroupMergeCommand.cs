using MediatR;

namespace Buddy.Application.CommandService.Group.StartMerge
{
    public class StartGroupMergeCommand: INotification
    {
        public StartGroupMergeCommand(string groupId, string matchedGroupId)
        {
            GroupId = groupId;
            MatchedGroupId = matchedGroupId;
        }

        public string GroupId { get; }
        public string MatchedGroupId { get; }
    }
}
