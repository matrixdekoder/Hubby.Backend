using MediatR;

namespace Buddy.Application.CommandService.Group.Merge
{
    public class MergeGroupCommand: INotification
    {
        public MergeGroupCommand(string groupId, string matchedGroupId)
        {
            GroupId = groupId;
            MatchedGroupId = matchedGroupId;
        }

        public string GroupId { get; }
        public string MatchedGroupId { get; }
    }
}
