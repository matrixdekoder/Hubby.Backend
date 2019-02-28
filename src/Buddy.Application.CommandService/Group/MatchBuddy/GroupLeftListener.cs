using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.MatchBuddy
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
            var command = new MatchBuddyCommand(notification.Id);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
