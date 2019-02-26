using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class GroupLeftListener: INotificationHandler<GroupLeft>
    {
        private readonly IProjectionWriter<BuddyReadModel> _writer;

        public GroupLeftListener(IProjectionWriter<BuddyReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(GroupLeft notification, CancellationToken cancellationToken)
        {
            await _writer.Update(notification.Id, view => view.GroupId = null);
        }
    }
}
