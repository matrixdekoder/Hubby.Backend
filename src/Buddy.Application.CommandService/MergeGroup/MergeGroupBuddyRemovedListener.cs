using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.MergeGroup
{
    public class MergeGroupBuddyRemovedListener: INotificationHandler<BuddyRemoved>
    {
        private readonly IMediator _mediator;

        public MergeGroupBuddyRemovedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddyRemoved notification, CancellationToken cancellationToken)
        {
            await _mediator.Publish(new MergeGroupCommand(notification.Id, notification.GroupIds), cancellationToken);
        }
    }
}
