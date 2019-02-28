using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.MatchGroup
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
            await _mediator.Publish(new MatchGroupCommand(notification.Id), cancellationToken);
        }
    }
}
