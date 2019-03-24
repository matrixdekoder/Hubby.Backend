using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class GroupIsClearedListener: INotificationHandler<GroupIsCleared>
    {
        private readonly IProjectionWriter _writer;

        public GroupIsClearedListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(GroupIsCleared notification, CancellationToken cancellationToken)
        {
            await _writer.Update<GroupReadModel>(notification.Id, view => view.BuddyIds.Clear());
        }
    }
}
