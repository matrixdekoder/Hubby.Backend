using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Account.Application.CommandService.LinkBuddy
{
    public class LinkBuddyWhenCreatedListener: INotificationHandler<BuddyCreated>
    {
        private readonly IMediator _mediator;

        public LinkBuddyWhenCreatedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddyCreated notification, CancellationToken cancellationToken)
        {
            var command = new LinkBuddyCommand(notification.AccountId, notification.Id);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
