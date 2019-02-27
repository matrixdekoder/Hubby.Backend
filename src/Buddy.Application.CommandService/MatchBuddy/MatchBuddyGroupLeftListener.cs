using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.MatchBuddy
{
    public class MatchBuddyGroupLeftListener: INotificationHandler<GroupLeft>
    {
        private readonly IMediator _mediator;

        public MatchBuddyGroupLeftListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupLeft notification, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new MatchBuddyCommand(notification.Id, notification.GroupIds), cancellationToken);
        }
    }
}
