using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Services;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.MatchBuddy
{
    public class MatchBuddyCommandHandler: INotificationHandler<MatchBuddyCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Buddy> _buddyRepository;
        private readonly IEventStoreRepository<Domain.Entities.Group> _groupRepository;
        private readonly IMatchService _matchService;

        public MatchBuddyCommandHandler(
            IEventStoreRepository<Domain.Entities.Buddy> buddyRepository, 
            IEventStoreRepository<Domain.Entities.Group> groupRepository,
            IMatchService matchService)
        {
            _buddyRepository = buddyRepository;
            _groupRepository = groupRepository;
            _matchService = matchService;
        }

        public async Task Handle(MatchBuddyCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _buddyRepository.GetById(notification.BuddyId);
            var groups = new List<Domain.Entities.Group>();

            foreach (var groupId in notification.GroupIds)
            {
                groups.Add(await _groupRepository.GetById(groupId));
            }

            var matchedGroup = await _matchService.GetBestGroup(buddy, groups);

            matchedGroup.AddBuddy(buddy);

            await _groupRepository.Save(matchedGroup);
            await _buddyRepository.Save(buddy);
        }
    }
}
