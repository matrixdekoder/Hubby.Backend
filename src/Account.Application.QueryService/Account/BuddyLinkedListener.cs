using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Application;
using MediatR;

namespace Account.Application.QueryService.Account
{
    public class BuddyLinkedListener: INotificationHandler<BuddyLinked>
    {
        private readonly IProjectionWriter<AccountReadModel> _writer;

        public BuddyLinkedListener(IProjectionWriter<AccountReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(BuddyLinked notification, CancellationToken cancellationToken)
        {
            await _writer.Update(notification.Id, view => view.BuddyId = notification.BuddyId);
        }
    }
}
