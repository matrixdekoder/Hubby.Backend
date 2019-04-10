using System.Threading.Tasks;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Group.SetStatus
{
    public class SetStatusGroupMatchedCommand: CommandListener<GroupMatched>
    {
        public SetStatusGroupMatchedCommand(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(GroupMatched notification)
        {
            var command = new SetGroupStatusCommand(notification.MatchId, GroupStatus.Merging);
            await Mediator.Publish(command);
        }
    }
}
