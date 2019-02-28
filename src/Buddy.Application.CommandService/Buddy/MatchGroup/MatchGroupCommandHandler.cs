using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Services;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.MatchGroup
{
    public class MatchGroupCommandHandler: INotificationHandler<MatchGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Buddy> _repository;
        private readonly IMatchService _matchService;

        public MatchGroupCommandHandler(
            IRepository<Domain.Entities.Buddy> repository,
            IMatchService matchService)
        {
            _repository = repository;
            _matchService = matchService;
        }

        public async Task Handle(MatchGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById(notification.BuddyId);
            var matchedGroupId = await _matchService.GetBestGroupId(buddy) ?? buddy.CreateGroup();
            buddy.JoinGroup(matchedGroupId);
            await _repository.Save(buddy);
        }
    }
}
