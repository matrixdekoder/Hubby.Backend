using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application;
using Region.Domain;

namespace Region.Application.QueryService.Listeners
{
    public class RegionCreatedListener : QueryListener<RegionCreated>
    {
        public RegionCreatedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(RegionCreated notification)
        {
            var view = new RegionReadModel
            {
                Id = notification.Id,
                Name = notification.Name,
                GroupIds = new List<string>()
            };

            await Writer.Add(view);
        }
    }
}
