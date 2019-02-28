using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class BuddyRemovedListener: INotificationHandler<BuddyRemoved>
    {
        private readonly IMediator _mediator;

        public BuddyRemovedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddyRemoved notification, CancellationToken cancellationToken)
        {
            var command = new LeaveGroupCommand(notification.BuddyId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
