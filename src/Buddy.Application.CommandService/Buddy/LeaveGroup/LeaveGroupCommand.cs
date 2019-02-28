using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class LeaveGroupCommand: INotification
    {
        public LeaveGroupCommand(string buddyId)
        {
            BuddyId = buddyId;
        }

        public string BuddyId { get; }
    }
}
