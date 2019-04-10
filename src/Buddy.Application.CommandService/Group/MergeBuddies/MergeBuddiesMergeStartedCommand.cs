using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class MergeBuddiesMergeStartedCommand: CommandListener<MergeStarted>
    {
        public MergeBuddiesMergeStartedCommand(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(MergeStarted notification)
        {
            var command = new MergeBuddiesCommand(notification.Id, notification.MatchedGroupId);
            await Mediator.Publish(command);
        }
    }
}
