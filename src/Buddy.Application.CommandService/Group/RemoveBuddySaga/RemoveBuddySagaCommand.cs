using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddySaga
{
    public class RemoveBuddySagaCommand: INotification
    {
        public RemoveBuddySagaCommand(string buddyId, string groupId)
        {
            BuddyId = buddyId;
            GroupId = groupId;
        }

        public string BuddyId { get; }
        public string GroupId { get; }
    }
}
