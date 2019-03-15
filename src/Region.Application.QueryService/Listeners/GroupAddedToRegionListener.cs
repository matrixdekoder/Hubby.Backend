using System.Threading;
using System.Threading.Tasks;
using Core.Application;
using MediatR;
using Region.Domain;

namespace Region.Application.QueryService.Listeners
{
    public class GroupAddedToRegionListener: INotificationHandler<GroupAddedToRegion>
    {
        private readonly IProjectionWriter _writer;

        public GroupAddedToRegionListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(GroupAddedToRegion notification, CancellationToken cancellationToken)
        {
            await _writer.Update<RegionReadModel>(notification.Id, x => x.GroupIds.Add(notification.GroupId));
        }
    }
}
