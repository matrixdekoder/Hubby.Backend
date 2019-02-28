using MediatR;

namespace Buddy.Application.CommandService.Group.StopMerge
{
    public class StopGroupMergeCommand: INotification
    {
        public StopGroupMergeCommand(string groupId, string matchedGroupId)
        {
            GroupId = groupId;
            MatchedGroupId = matchedGroupId;
        }

        public string GroupId { get; }
        public string MatchedGroupId { get; }
    }
}
