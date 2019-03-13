using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class GroupLeftListener: INotificationHandler<GroupLeft>
    {
        private readonly IProjectionWriter _writer;

        public GroupLeftListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(GroupLeft notification, CancellationToken cancellationToken)
        {
            await _writer.Update<BuddyReadModel>(notification.Id, view => view.GroupId = null);
        }
    }
}
