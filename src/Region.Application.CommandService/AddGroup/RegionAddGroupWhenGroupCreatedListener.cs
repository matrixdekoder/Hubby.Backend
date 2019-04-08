using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Region.Application.CommandService.AddGroup
{
    public class RegionAddGroupWhenGroupCreatedListener : CommandListener<GroupCreated>
    {
        public RegionAddGroupWhenGroupCreatedListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(GroupCreated notification, CancellationToken cancellationToken)
        {
            var command = new RegionAddGroupCommand(notification.RegionId, notification.Id);
            await Mediator.Publish(command, cancellationToken);
        }
    }
}
