using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class CreateBuddyCommand: INotification
    {
        public CreateBuddyCommand(string accountId, string buddyId)
        {
            AccountId = accountId;
            BuddyId = buddyId;
        }

        public string AccountId { get; }
        public string BuddyId { get; }
    }
}
