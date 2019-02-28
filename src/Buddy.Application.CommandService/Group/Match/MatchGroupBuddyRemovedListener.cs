using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.Match
{
    public class MatchGroupBuddyRemovedListener: INotificationHandler<BuddyRemoved>
    {
        private readonly IMediator _mediator;

        public MatchGroupBuddyRemovedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddyRemoved notification, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new MatchGroupCommand(notification.Id), cancellationToken);
        }
    }
}
