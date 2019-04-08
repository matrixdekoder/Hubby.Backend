using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.CommandService.Group.StartMerge
{
    public class StartGroupWhenGroupMatchedListener: CommandListener<GroupMatched>
    {
        public StartGroupWhenGroupMatchedListener(IMediator mediator):base(mediator)
        {
        }

        protected override async Task Handle(GroupMatched notification, CancellationToken cancellationToken)
        {
            var command = new StartGroupMergeCommand(notification.Id, notification.MatchId);
            await Mediator.Publish(command, cancellationToken);
        }
    }
}
