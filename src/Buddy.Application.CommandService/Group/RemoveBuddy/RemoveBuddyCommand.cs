using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddy
{
    public class RemoveBuddyCommand: INotification
    {
        public RemoveBuddyCommand(string buddyId, string groupId)
        {
            BuddyId = buddyId;
            GroupId = groupId;
        }

        public string BuddyId { get; }
        public string GroupId { get; }
    }
}
