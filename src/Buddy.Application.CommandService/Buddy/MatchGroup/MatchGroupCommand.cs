using MediatR;

namespace Buddy.Application.CommandService.Buddy.MatchGroup
{
    public class MatchGroupCommand: INotification
    {
        public MatchGroupCommand(string buddyId)
        {
            BuddyId = buddyId;
        }

        public string BuddyId { get; }
    }
}
