using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class GroupMergedListener: INotificationHandler<GroupMerged>
    {
        private readonly IMediator _mediator;

        public GroupMergedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupMerged notification, CancellationToken cancellationToken)
        {
            var command = new MergeBuddiesCommand(notification.Id, notification.MatchedGroupId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
