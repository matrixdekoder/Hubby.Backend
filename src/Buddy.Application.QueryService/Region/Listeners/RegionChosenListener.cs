using System.Threading.Tasks;
using Buddy.Application.QueryService.Buddy;
using Buddy.Domain.Events;
using Core.Application;

namespace Buddy.Application.QueryService.Region.Listeners
{
    public class RegionChosenListener : QueryListener<RegionChosen>
    {
        public RegionChosenListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(RegionChosen notification)
        {
            await Writer.Update<BuddyReadModel>(notification.Id, x => x.RegionId = notification.RegionId);
        }
    }
}
