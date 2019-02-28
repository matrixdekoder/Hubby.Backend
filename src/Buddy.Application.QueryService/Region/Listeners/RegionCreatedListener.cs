using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Region.Listeners
{
    public class RegionCreatedListener: INotificationHandler<RegionCreated>
    {
        private readonly IProjectionWriter<RegionReadModel> _writer;

        public RegionCreatedListener(IProjectionWriter<RegionReadModel> writer)
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
