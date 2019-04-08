using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Application;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class CreateBuddyAccountRegisteredListener: CommandListener<AccountRegistered>
    {
        public CreateBuddyAccountRegisteredListener(IMediator mediator): base(mediator)
        {
        }

        protected override async Task Handle(AccountRegistered notification, CancellationToken cancellationToken)
        {
            var command = new CreateBuddyCommand(notification.Id);
            await Mediator.Publish(command, cancellationToken);
        }
    }
}
