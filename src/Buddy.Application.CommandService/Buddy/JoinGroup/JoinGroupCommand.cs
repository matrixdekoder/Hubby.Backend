using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class JoinGroupCommand: INotification
    {
        public JoinGroupCommand(string buddyId, string groupId, bool isMerge)
        {
            BuddyId = buddyId;
            GroupId = groupId;
            IsMerge = isMerge;
        }

        public string BuddyId { get; }
        public string GroupId { get; }
        public bool IsMerge { get; }
    }
}
