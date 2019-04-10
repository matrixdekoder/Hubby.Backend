using System.Threading.Tasks;
using Core.Application.Query;
using Region.Domain;

namespace Region.Application.QueryService.Listeners
{
    public class GroupAddedToRegionListener : QueryListener<GroupAddedToRegion>
    {
        public GroupAddedToRegionListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(GroupAddedToRegion notification)
        {
            await Writer.Update<RegionReadModel>(notification.Id, x => x.GroupIds.Add(notification.GroupId));
        }
    }
}
