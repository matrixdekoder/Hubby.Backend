using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Group.StartMerge
{
    public class StartMergeGroupMatchedListener: CommandListener<GroupMatched>
    {
        public StartMergeGroupMatchedListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(GroupMatched notification)
        {
            var command = new StartGroupMergeCommand(notification.Id, notification.MatchId);
            await Mediator.Publish(command);
        }
    }
}
