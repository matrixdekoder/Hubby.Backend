using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.Merge
{
    public class GroupMatchedListener: INotificationHandler<GroupMatched>
    {
        private readonly IMediator _mediator;

        public GroupMatchedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupMatched notification, CancellationToken cancellationToken)
        {
            var command = new MergeGroupCommand(notification.Id, notification.MatchId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
