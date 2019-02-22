using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy
{
    public class BuddyCreatedListener: INotificationHandler<BuddyCreated>
    {
        private readonly IProjectionWriter<BuddyReadModel> _writer;

        public BuddyCreatedListener(IProjectionWriter<BuddyReadModel> writer)
        {
            _writer = writer;
        }

        public async Task Handle(BuddyCreated notification, CancellationToken cancellationToken)
        {
            var view = new BuddyReadModel
            {
                Id = notification.Id
            };

            await _writer.Add(view);
        }
    }
}
