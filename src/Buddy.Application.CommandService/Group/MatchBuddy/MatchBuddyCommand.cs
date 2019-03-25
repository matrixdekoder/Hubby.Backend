using MediatR;

namespace Buddy.Application.CommandService.Group.MatchBuddy
{
    public class MatchBuddyCommand: INotification
    {
        public MatchBuddyCommand(string buddyId)
        {
            BuddyId = buddyId;
        }

        public string BuddyId { get; }
    }
}
