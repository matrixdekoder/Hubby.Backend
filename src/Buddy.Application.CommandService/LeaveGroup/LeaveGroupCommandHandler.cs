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
            var group = await _groupRepository.GetById(buddy.CurrentGroupId);

            buddy.LeaveGroup();
            group.RemoveBuddy(buddy.Id);

            await _buddyRepository.Save(buddy);
            await _groupRepository.Save(group);

            await _mediator.Publish(new MatchBuddyCommand(buddy.Id, notification.GroupIds), cancellationToken);
            await _mediator.Publish(new MergeGroupCommand(group.Id, notification.GroupIds), cancellationToken);
        }
    }
}
