using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.CommandService.Group.Match
{
    public class MatchGroupBuddyRemovedListener: CommandListener<BuddyRemoved>
    {
        public MatchGroupBuddyRemovedListener(IMediator mediator): base(mediator)
        {
        }

        protected override async Task Handle(BuddyRemoved notification, CancellationToken cancellationToken)
        {
            await Mediator.Publish(new MatchGroupCommand(notification.Id), cancellationToken);
        }
    }
}
