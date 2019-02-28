using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Region.AddGroup
{
    public class GroupCreatedListener: INotificationHandler<GroupCreated>
    {
        private readonly IMediator _mediator;

        public GroupCreatedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(GroupCreated notification, CancellationToken cancellationToken)
        {
            var command = new RegionAddGroupCommand(notification.RegionId, notification.Id);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}
