using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class JoinGroupBuddyAddedListener: INotificationHandler<BuddyAdded>
    {
        private readonly IMediator _mediator;

        public JoinGroupBuddyAddedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddyAdded notification, CancellationToken cancellationToken)
        {
            var command = new JoinGroupCommand(notification.BuddyId, notification.Id, notification.IsMerge);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
