using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.RemoveBuddy
{
    public class RemoveBuddyGroupLeftListener: INotificationHandler<GroupLeft>
    {
        private readonly IMediator _mediator;

        public RemoveBuddyGroupLeftListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupLeft notification, CancellationToken cancellationToken)
        {
            var command = new RemoveBuddyCommand(notification.Id, notification.GroupId, notification.GroupIds);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
