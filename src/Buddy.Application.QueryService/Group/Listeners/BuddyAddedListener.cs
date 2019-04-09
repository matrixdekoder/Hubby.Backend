using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Group.Listeners
{
    public class BuddyAddedListener : QueryListener<BuddyAdded>
    {
        public BuddyAddedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(BuddyAdded notification)
        {
            await Writer.Update<GroupReadModel>(notification.Id, view => view.BuddyIds.Add(notification.BuddyId));
        }
    }
}
