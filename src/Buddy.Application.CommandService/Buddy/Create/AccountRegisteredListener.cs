using System.Threading.Tasks;
using Account.Domain;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class AccountRegisteredListener: CommandListener<AccountRegistered>
    {
        public AccountRegisteredListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(AccountRegistered notification)
        {
            var command = new CreateBuddyCommand(notification.Id, notification.BuddyId);
            await Mediator.Publish(command);
        }
    }
}
