using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class GroupIsClearedListener: QueryListener<GroupIsCleared>
    {
        public GroupIsClearedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(GroupIsCleared notification)
        {
            await Writer.Update<GroupReadModel>(notification.Id, view => view.BuddyIds.Clear());
        }
    }
}
