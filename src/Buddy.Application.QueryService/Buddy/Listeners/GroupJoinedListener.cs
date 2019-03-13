using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class GroupJoinedListener: INotificationHandler<GroupJoined>
    {
        private readonly IProjectionWriter _writer;

        public GroupJoinedListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(GroupJoined notification, CancellationToken cancellationToken)
        {
            await _writer.Update<BuddyReadModel>(notification.Id, view => view.GroupId = notification.GroupId);
        }
    }
}
