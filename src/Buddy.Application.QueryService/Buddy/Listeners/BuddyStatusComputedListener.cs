using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class BuddyStatusComputedListener: INotificationHandler<StatusComputed>
    {
        private readonly IProjectionWriter _writer;

        public BuddyStatusComputedListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(StatusComputed notification, CancellationToken cancellationToken)
        {
            await _writer.Update<BuddyReadModel>(notification.Id, view => view.Status = notification.Status);
        }
    }
}
