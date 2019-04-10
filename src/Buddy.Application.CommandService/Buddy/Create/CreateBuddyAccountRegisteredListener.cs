using System.Threading.Tasks;
using Account.Domain;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class CreateBuddyAccountRegisteredListener: CommandListener<AccountRegistered>
    {
        public CreateBuddyAccountRegisteredListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(AccountRegistered notification)
        {
            var command = new CreateBuddyCommand(notification.Id, notification.BuddyId);
            await Mediator.Publish(command);
        }
    }
}
