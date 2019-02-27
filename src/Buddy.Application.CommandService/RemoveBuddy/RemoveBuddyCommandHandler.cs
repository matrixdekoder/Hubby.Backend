using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Entities;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.RemoveBuddy
{
    class RemoveBuddyCommandHandler: INotificationHandler<RemoveBuddyCommand>
    {
        private readonly IEventStoreRepository<Group> _repository;

        public RemoveBuddyCommandHandler(IEventStoreRepository<Group> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBuddyCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(notification.GroupId);
            group.RemoveBuddy(notification.BuddyId, notification.GroupIds);
            await _repository.Save(group);
        }
    }
}
