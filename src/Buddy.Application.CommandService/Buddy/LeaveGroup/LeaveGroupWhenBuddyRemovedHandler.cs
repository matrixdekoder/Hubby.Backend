using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class LeaveGroupWhenBuddyRemovedHandler: CommandListener<BuddyRemoved>
    {
        public LeaveGroupWhenBuddyRemovedHandler(IMediator mediator): base(mediator)
        {
        }

        protected override async Task Handle(BuddyRemoved notification, CancellationToken cancellationToken)
        {
            var command = new LeaveGroupCommand((notification.BuddyId));
            await Mediator.Publish(command, cancellationToken);
        }
    }
}
