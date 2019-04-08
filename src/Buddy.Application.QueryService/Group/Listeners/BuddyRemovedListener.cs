using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class BuddyRemovedListener: QueryListener<BuddyRemoved>
    {
        public BuddyRemovedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(BuddyRemoved notification)
        {
            await Writer.Update<GroupReadModel>(notification.Id, view => view.BuddyIds.Remove(notification.BuddyId));
        }
    }
}
