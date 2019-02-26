using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class BuddyStatusComputedListener: INotificationHandler<StatusComputed>
    {
        private readonly IProjectionWriter<BuddyReadModel> _writer;

        public BuddyStatusComputedListener(IProjectionWriter<BuddyReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(StatusComputed notification, CancellationToken cancellationToken)
        {
            await _writer.Update(notification.Id, view => view.Status = notification.Status);
        }
    }
}
