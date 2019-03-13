using System.Threading;
using System.Threading.Tasks;
using Buddy.Application.QueryService.Buddy;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Region.Listeners
{
    public class RegionChosenListener: INotificationHandler<RegionChosen>
    {
        private readonly IProjectionWriter _writer;

        public RegionChosenListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(RegionChosen notification, CancellationToken cancellationToken)
        {
            await _writer.Update<BuddyReadModel>(notification.Id, x => x.RegionId = notification.RegionId);
        }
    }
}
