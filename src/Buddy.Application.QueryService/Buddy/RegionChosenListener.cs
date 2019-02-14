using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy
{
    public class RegionChosenListener: INotificationHandler<RegionChosen>
    {
        private readonly IProjectionWriter<BuddyReadModel> _writer;

        public RegionChosenListener(IProjectionWriter<BuddyReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(RegionChosen notification, CancellationToken cancellationToken)
        {
            await _writer.Update(notification.Id, x => x.RegionId = notification.RegionId);
        }
    }
}
