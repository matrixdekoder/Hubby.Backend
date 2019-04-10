using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Group.Clear
{
    public class ClearGroupBuddiesMergedListener: CommandListener<BuddiesMerged>
    {
        public ClearGroupBuddiesMergedListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(BuddiesMerged notification)
        {
            var command = new ClearGroupCommand(notification.MatchedGroupId);
            await Mediator.Publish(command);
        }
    }
}
