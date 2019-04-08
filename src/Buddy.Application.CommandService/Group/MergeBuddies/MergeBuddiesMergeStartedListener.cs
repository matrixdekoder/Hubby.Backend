using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class MergeBuddiesMergeStartedListener : CommandListener<MergeStarted>
    {
        public MergeBuddiesMergeStartedListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(MergeStarted notification, CancellationToken cancellationToken)
        {
            var command = new MergeBuddiesCommand(notification.Id, notification.MatchedGroupId);
            await Mediator.Publish(command, cancellationToken);
        }
    }
}
