using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class BuddyAddedListener: INotificationHandler<BuddyAdded>
    {
        private readonly IProjectionWriter<GroupReadModel> _writer;

        public BuddyAddedListener(IProjectionWriter<GroupReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(BuddyAdded notification, CancellationToken cancellationToken)
        {
            await _writer.Update(notification.Id, view => view.BuddyIds.Add(notification.BuddyId));
        }
    }
}
