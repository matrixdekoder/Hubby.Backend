using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddy
{
    public class GroupLeftListener: INotificationHandler<GroupLeft>
    {
        private readonly IMediator _mediator;

        public GroupLeftListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupLeft notification, CancellationToken cancellationToken)
        {
            var command = new RemoveBuddyCommand(notification.Id, notification.GroupId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
