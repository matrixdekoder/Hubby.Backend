using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.SetStatus
{
    public class SetGroupStatusMergeStartedListener: INotificationHandler<MergeStarted>
    {
        private readonly IMediator _mediator;

        public SetGroupStatusMergeStartedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(MergeStarted notification, CancellationToken cancellationToken)
        {
            var command = new SetGroupStatusCommand(notification.MatchedGroupId, GroupStatus.Merging);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
