using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using Core.Application;
using MediatR;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class BuddyCreatedListener: INotificationHandler<BuddyCreated>
    {
        private readonly IProjectionWriter _writer;

        public BuddyCreatedListener(IProjectionWriter writer)
        {
            _writer = writer;
        }

        public async Task Handle(BuddyCreated notification, CancellationToken cancellationToken)
        {
            var view = new BuddyReadModel
            {
                Id = notification.Id,
                AccountId = notification.AccountId,
                GenreIds = new List<string>()
            };

            await _writer.Add(view);
        }
    }
}
