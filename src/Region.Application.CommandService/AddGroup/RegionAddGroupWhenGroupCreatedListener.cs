using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Region.Application.CommandService.AddGroup
{
    public class RegionAddGroupWhenGroupCreatedListener: INotificationHandler<GroupCreated>
    {
        private readonly IMediator _mediator;

        public RegionAddGroupWhenGroupCreatedListener(IMediator mediator)
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
