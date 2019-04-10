using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class LeaveGroupBuddyRemovedListener: CommandListener<BuddyRemoved>
    {
        public LeaveGroupBuddyRemovedListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(BuddyRemoved notification)
        {
            var command = new LeaveGroupCommand(notification.BuddyId);
            await Mediator.Publish(command);
        }
    }
}
