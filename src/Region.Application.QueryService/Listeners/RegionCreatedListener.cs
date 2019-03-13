using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Application;
using MediatR;
using Region.Domain;

namespace Region.Application.QueryService.Listeners
{
    public class RegionCreatedListener: INotificationHandler<RegionCreated>
    {
        private readonly IProjectionWriter _writer;

        public RegionCreatedListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(RegionCreated notification, CancellationToken cancellationToken)
        {
            var view = new RegionReadModel
            {
                Id = notification.Id,
                Name = notification.Name,
                GroupIds = new List<string>()
            };

            await _writer.Add(view);
        }
    }
}
