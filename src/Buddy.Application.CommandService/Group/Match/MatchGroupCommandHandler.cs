using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.Match
{
    public class MatchGroupCommandHandler : INotificationHandler<MatchGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _groupRepository;
        private readonly IRepository<Domain.Entities.Region> _regionRepository;
        private readonly IRepository<Domain.Entities.Buddy> _buddyRepository;

        public MatchGroupCommandHandler(
            IRepository<Domain.Entities.Group> groupRepository,
            IRepository<Domain.Entities.Region> regionRepository,
            IRepository<Domain.Entities.Buddy> buddyRepository)
        {
            _groupRepository = groupRepository;
            _regionRepository = regionRepository;
            _buddyRepository = buddyRepository;
        }

        public async Task Handle(MatchGroupCommand notification, CancellationToken cancellationToken)
        {
            var currentGroup = await _groupRepository.GetById(notification.GroupId);
            var region = await _regionRepository.GetById(currentGroup.RegionId);
            var otherGroupIds = region.GroupIds.Except(new List<string> { currentGroup.Id });
            var otherGroups = new List<Domain.Entities.Group>();

            foreach (var groupId in otherGroupIds)
            {
                var otherGroup = await _groupRepository.GetById(groupId);
                otherGroups.Add(otherGroup);
            }

            currentGroup.Match(otherGroups);
            await _groupRepository.Save(currentGroup);
        }
    }
}
