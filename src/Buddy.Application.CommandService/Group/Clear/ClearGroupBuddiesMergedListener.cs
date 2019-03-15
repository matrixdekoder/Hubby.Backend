using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.Clear
{
    public class ClearGroupBuddiesMergedListener: INotificationHandler<BuddiesMerged>
    {
        private readonly IMediator _mediator;

        public ClearGroupBuddiesMergedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddiesMerged notification, CancellationToken cancellationToken)
        {
            var command = new ClearGroupCommand(notification.MatchedGroupId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
