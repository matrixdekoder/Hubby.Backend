using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddy
{
    public class RemoveBuddyCommandHandler: INotificationHandler<RemoveBuddyCommand>
    {
        private readonly ISagaRepository _repository;

        public RemoveBuddyCommandHandler(ISagaRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBuddyCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById<Domain.Group>(notification.GroupId);
            group.RemoveBuddy(notification.BuddyId);
            await _repository.Save(notification.TransactionId, group);
        }
    }
}
