using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class CreateBuddyCommand: INotification
    {
        public CreateBuddyCommand(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; }
    }
}
