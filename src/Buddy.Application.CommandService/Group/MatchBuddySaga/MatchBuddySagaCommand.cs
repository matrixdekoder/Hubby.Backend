using MediatR;

namespace Buddy.Application.CommandService.Group.MatchBuddySaga
{
    public class MatchBuddySagaCommand: INotification
    {
        public MatchBuddySagaCommand(string buddyId)
        {
            BuddyId = buddyId;
        }

        public string BuddyId { get; }
    }
}
