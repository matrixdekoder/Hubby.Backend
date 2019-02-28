using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class BuddyAddedListener: INotificationHandler<BuddyAdded>
    {
        private readonly IMediator _mediator;

        public BuddyAddedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddyAdded notification, CancellationToken cancellationToken)
        {
            var command = new JoinGroupCommand(notification.BuddyId, notification.Id);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
