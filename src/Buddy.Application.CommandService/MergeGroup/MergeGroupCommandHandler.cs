using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Services;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.MergeGroup
{
    public class MergeGroupCommandHandler: INotificationHandler<MergeGroupCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Group> _groupRepository;
        private readonly IMatchService _matchService;

        public MergeGroupCommandHandler(
            IEventStoreRepository<Domain.Entities.Group> groupRepository,
            IMatchService matchService)
        {
            _groupRepository = groupRepository;
            _matchService = matchService;
        }

        public async Task Handle(MergeGroupCommand notification, CancellationToken cancellationToken)
        {
            var currentGroup = await _groupRepository.GetById(notification.GroupId);
            var otherGroups = new List<Domain.Entities.Group>();

            foreach (var groupId in notification.OtherGroupIds)
            {
                var otherGroup = await _groupRepository.GetById(groupId);
                otherGroups.Add(otherGroup);
            }

            var matchedGroup = currentGroup.Match(otherGroups);
            if(matchedGroup != null)
                await _matchService.MergeGroups(currentGroup, matchedGroup, notification.OtherGroupIds);
        }
    }
}
