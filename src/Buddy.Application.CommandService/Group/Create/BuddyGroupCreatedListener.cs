using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.Create
{
    public class BuddyGroupCreatedListener: INotificationHandler<BuddyGroupCreated>
    {
        private readonly IMediator _mediator;

        public BuddyGroupCreatedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddyGroupCreated notification, CancellationToken cancellationToken)
        {
            var command = new CreateGroupCommand(notification.GroupId, notification.RegionId, notification.GenreIds);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
