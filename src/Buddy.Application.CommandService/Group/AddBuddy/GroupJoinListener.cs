using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.AddBuddy
{
    public class GroupJoinListener: INotificationHandler<GroupJoined>
    {
        private readonly IMediator _mediator;

        public GroupJoinListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupJoined notification, CancellationToken cancellationToken)
        {
            var command = new AddBuddyCommand(notification.GroupId, notification.Id);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
