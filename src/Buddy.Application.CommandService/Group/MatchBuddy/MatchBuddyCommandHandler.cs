using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Services;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.MatchBuddy
{
    public class MatchBuddyCommandHandler: INotificationHandler<MatchBuddyCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _groupRepository;
        private readonly IRepository<Domain.Entities.Buddy> _buddyRepository;
        private readonly IMatchService _matchService;

        public MatchBuddyCommandHandler(
            IRepository<Domain.Entities.Group> groupRepository,
            IRepository<Domain.Entities.Buddy> buddyRepository,
            IMatchService matchService)
        {
            _groupRepository = groupRepository;
            _buddyRepository = buddyRepository;
            _matchService = matchService;
        }

        public async Task Handle(MatchBuddyCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _buddyRepository.GetById(notification.BuddyId);
            var group = await _matchService.GetBestGroup(buddy);
            group.AddBuddy(buddy);
            await _groupRepository.Save(group);
        }
    }
}
