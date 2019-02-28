using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            var matchedGroupBuddies = await GetBuddies(matchedGroup.BuddyIds);

            group.MergeBuddies(matchedGroup, matchedGroupBuddies);

            await _groupRepository.Save(matchedGroup);
            await _groupRepository.Save(group);
        }

        private async Task<IEnumerable<Domain.Entities.Buddy>> GetBuddies(IEnumerable<string> buddyIds)
        {
            var tasks = await Task.WhenAll(buddyIds.Select(x => _buddyRepository.GetById(x)));
            return tasks.Where(task => task != null).ToList();
        }
    }
}
