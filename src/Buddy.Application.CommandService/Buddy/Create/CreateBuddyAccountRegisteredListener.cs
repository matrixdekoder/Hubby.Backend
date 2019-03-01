using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class CreateBuddyAccountRegisteredListener: INotificationHandler<AccountRegistered>
    {
        private readonly IMediator _mediator;

        public CreateBuddyAccountRegisteredListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(AccountRegistered notification, CancellationToken cancellationToken)
        {
            var command = new CreateBuddyCommand(notification.Id);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
