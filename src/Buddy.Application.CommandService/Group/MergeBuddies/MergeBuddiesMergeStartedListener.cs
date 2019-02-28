using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class MergeBuddiesMergeStartedListener: INotificationHandler<MergeStarted>
    {
        private readonly IMediator _mediator;

        public MergeBuddiesMergeStartedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(MergeStarted notification, CancellationToken cancellationToken)
        {
            var command = new MergeBuddiesCommand(notification.Id, notification.MatchedGroupId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
