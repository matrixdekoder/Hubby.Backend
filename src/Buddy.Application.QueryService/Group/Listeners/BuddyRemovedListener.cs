using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class BuddyRemovedListener: INotificationHandler<BuddyRemoved>
    {
        private readonly IProjectionWriter _writer;

        public BuddyRemovedListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(BuddyRemoved notification, CancellationToken cancellationToken)
        {
            await _writer.Update<GroupReadModel>(notification.Id, view => view.BuddyIds.Remove(notification.BuddyId));
        }
    }
}
