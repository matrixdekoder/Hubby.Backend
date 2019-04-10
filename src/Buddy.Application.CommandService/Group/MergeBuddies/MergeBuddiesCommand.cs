using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class MergeBuddiesCommand: INotification
    {
        public MergeBuddiesCommand(string groupId, string matchedGroupId)
        {
            GroupId = groupId;
            MatchedGroupId = matchedGroupId;
        }

        public string GroupId { get; }
        public string MatchedGroupId { get; }
    }
}
