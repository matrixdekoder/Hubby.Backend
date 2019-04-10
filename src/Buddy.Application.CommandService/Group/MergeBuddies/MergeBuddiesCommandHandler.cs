using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Enums;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class MergeBuddiesCommandHandler: INotificationHandler<MergeBuddiesCommand>
    {
        private readonly IRepository _repository;

        public MergeBuddiesCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(MergeBuddiesCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById<Domain.Group>(notification.GroupId);

            var matchedGroup = await _repository.GetById<Domain.Group>(notification.MatchedGroupId);
            var matchedGroupBuddies = await GetBuddies(matchedGroup.BuddyIds);

            group.MergeBuddies(matchedGroup, matchedGroupBuddies);
            group.SetStatus(GroupStatus.Open);

            await _repository.Save(group);
        }

        private async Task<IEnumerable<Domain.Buddy>> GetBuddies(IEnumerable<string> buddyIds)
        {
            var tasks = await Task.WhenAll(buddyIds.Select(x => _repository.GetById<Domain.Buddy>(x)));
            return tasks.Where(task => task != null).ToList();
        }
    }
}
