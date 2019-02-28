using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Enums;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class MergeBuddiesCommandHandler: INotificationHandler<MergeBuddiesCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _groupRepository;
        private readonly IRepository<Domain.Entities.Buddy> _buddyRepository;

        public MergeBuddiesCommandHandler(IRepository<Domain.Entities.Group> groupRepository, IRepository<Domain.Entities.Buddy> buddyRepository)
        {
            _groupRepository = groupRepository;
            _buddyRepository = buddyRepository;
        }

        public async Task Handle(MergeBuddiesCommand notification, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetById(notification.GroupId);
            var matchedGroup = await _groupRepository.GetById(notification.MatchedGroupId);

            foreach (var buddyId in matchedGroup.BuddyIds)
            {
                var buddy = await _buddyRepository.GetById(buddyId);
                buddy.LeaveGroup();
                buddy.JoinGroup(group.Id);
                await _buddyRepository.Save(buddy);
            }

            group.SetStatus(GroupStatus.Open);
            matchedGroup.Reset();

            await _groupRepository.Save(group);
            await _groupRepository.Save(matchedGroup);
        }
    }
}
