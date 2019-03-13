using System.Threading;
using System.Threading.Tasks;
using Account.Domain;
using Core.Application;
using MediatR;

namespace Account.Application.QueryService.Account.Listeners
{
    public class BuddyLinkedListener: INotificationHandler<BuddyLinked>
    {
        private readonly IProjectionWriter _writer;

        public BuddyLinkedListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(BuddyLinked notification, CancellationToken cancellationToken)
        {
            await _writer.Update<AccountReadModel>(notification.Id, view => view.BuddyId = notification.BuddyId);
        }
    }
}
