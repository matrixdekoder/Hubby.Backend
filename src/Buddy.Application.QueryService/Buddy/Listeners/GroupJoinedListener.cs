using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class GroupJoinedListener : QueryListener<GroupJoined>
    {
        public GroupJoinedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(GroupJoined notification)
        {
            await Writer.Update<BuddyReadModel>(notification.Id, view => view.GroupId = notification.GroupId);
        }
    }
}
