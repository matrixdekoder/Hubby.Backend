using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Application.CommandService.MatchBuddy;
using Buddy.Application.CommandService.MergeGroup;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.LeaveGroup
{
    public class LeaveGroupCommandHandler: INotificationHandler<LeaveGroupCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Buddy> _buddyRepository;
        private readonly IEventStoreRepository<Domain.Entities.Group> _groupRepository;
        private readonly IMediator _mediator;

        public LeaveGroupCommandHandler(
            IEventStoreRepository<Domain.Entities.Buddy> buddyRepository,
            IEventStoreRepository<Domain.Entities.Group> groupRepository,
            IMediator mediator
            )
        {
            _buddyRepository = buddyRepository;
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        public async Task Handle(LeaveGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _buddyRepository.GetById(notification.BuddyId);
            buddy.LeaveGroup(notification.GroupIds);
            await _buddyRepository.Save(buddy);
        }
    }
}
