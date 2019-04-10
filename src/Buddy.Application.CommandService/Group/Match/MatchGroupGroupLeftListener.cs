using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application.Command;
using MediatR;

namespace Buddy.Application.CommandService.Group.Match
{
    public class MatchGroupGroupLeftListener: CommandListener<GroupLeft>
    {
        public MatchGroupGroupLeftListener(IMediator mediator) : base(mediator)
        {
        }

        protected override async Task Handle(GroupLeft notification)
        {
            var command = new MatchGroupCommand(notification.GroupId);
            await Mediator.Publish(command);
        }
    }
}
