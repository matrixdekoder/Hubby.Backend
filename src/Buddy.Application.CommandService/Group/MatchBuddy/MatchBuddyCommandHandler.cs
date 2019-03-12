using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Services;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.MatchBuddy
{
    public class MatchBuddyCommandHandler: INotificationHandler<MatchBuddyCommand>
    {
        private readonly IRepository _repository;
        private readonly IMatchService _matchService;

        public MatchBuddyCommandHandler(
            IRepository repository,
            IMatchService matchService)
        {
            _repository = repository;
            _matchService = matchService;
        }

        public async Task Handle(MatchBuddyCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Entities.Buddy>(notification.BuddyId);
            var group = await _matchService.GetBestGroup(buddy);
            group.AddBuddy(buddy);
            await _repository.Save(group);
        }
    }
}
