using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;

namespace Region.Application.CommandService.AddGroup
{
    public class GroupCreatedRegionListener: CommandListener<GroupCreated>
    {
        public GroupCreatedRegionListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(GroupCreated notification)
        {
            var command = new RegionAddGroupCommand(notification.RegionId, notification.Id);
            await Mediator.Publish(command);
        }
    }
}
