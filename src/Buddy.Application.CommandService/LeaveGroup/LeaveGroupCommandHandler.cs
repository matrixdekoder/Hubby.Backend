using System.Threading;
using System.Threading.Tasks;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.LeaveGroup
{
    public class LeaveGroupCommandHandler: INotificationHandler<LeaveGroupCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Buddy> _buddyRepository;
        private readonly IEventStoreRepository<Domain.Entities.Group> _groupRepository;

        public LeaveGroupCommandHandler(
            IEventStoreRepository<Domain.Entities.Buddy> buddyRepository,
            IEventStoreRepository<Domain.Entities.Group> groupRepository
            )
        {
            _buddyRepository = buddyRepository;
            _groupRepository = groupRepository;
        }

        public async Task Handle(LeaveGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _buddyRepository.GetById(notification.BuddyId);
            var group = await _groupRepository.GetById(buddy.CurrentGroupId);

            buddy.LeaveGroup();
            group.RemoveBuddy(buddy.Id);

            await _buddyRepository.Save(buddy);
            await _groupRepository.Save(group);
        }
    }
}
