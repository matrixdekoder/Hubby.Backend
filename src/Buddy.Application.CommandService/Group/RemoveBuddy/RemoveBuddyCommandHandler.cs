using System.Threading;
using System.Threading.Tasks;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddy
{
    class RemoveBuddyCommandHandler: INotificationHandler<RemoveBuddyCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Group> _repository;

        public RemoveBuddyCommandHandler(IEventStoreRepository<Domain.Entities.Group> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBuddyCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(notification.GroupId);
            group.RemoveBuddy(notification.BuddyId);
            await _repository.Save(group);
        }
    }
}
