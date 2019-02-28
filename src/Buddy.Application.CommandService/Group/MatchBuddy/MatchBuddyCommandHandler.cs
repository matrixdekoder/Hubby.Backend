using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Services;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.MatchBuddy
{
    public class MatchBuddyCommandHandler: INotificationHandler<MatchBuddyCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _repository;
        private readonly IMatchService _matchService;

        public MatchBuddyCommandHandler(IRepository<Domain.Entities.Group> repository, IMatchService matchService)
        {
            _repository = repository;
            _matchService = matchService;
        }

        public async Task Handle(MatchBuddyCommand notification, CancellationToken cancellationToken)
        {
            var group = await _matchService.GetBestGroup(notification.BuddyId);
            group.AddBuddy(notification.BuddyId);
            await _repository.Save(group);
        }
    }
}
