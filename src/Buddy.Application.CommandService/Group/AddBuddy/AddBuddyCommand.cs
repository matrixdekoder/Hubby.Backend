using MediatR;

namespace Buddy.Application.CommandService.Group.AddBuddy
{
    public class AddBuddyCommand: INotification
    {
        public AddBuddyCommand(string groupId, string buddyId)
        {
            GroupId = groupId;
            BuddyId = buddyId;
        }

        public string GroupId { get; }
        public string BuddyId { get; }
    }
}
