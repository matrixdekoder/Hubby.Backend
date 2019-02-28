using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.StartMerge
{
    public class StartGroupWhenGroupMatchedListener: INotificationHandler<GroupMatched>
    {
        private readonly IMediator _mediator;

        public StartGroupWhenGroupMatchedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupMatched notification, CancellationToken cancellationToken)
        {
            var command = new StartGroupMergeCommand(notification.Id, notification.MatchId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
