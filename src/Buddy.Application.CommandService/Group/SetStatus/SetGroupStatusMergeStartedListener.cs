using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.CommandService.Group.SetStatus
{
    public class SetGroupStatusMergeStartedListener : CommandListener<MergeStarted>
    {
        protected SetGroupStatusMergeStartedListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(MergeStarted notification, CancellationToken cancellationToken)
        {
            var command = new SetGroupStatusCommand(notification.MatchedGroupId, GroupStatus.Merging);
            await Mediator.Publish(command, cancellationToken);
        }
    }
}
