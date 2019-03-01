using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;
using Region.Application.QueryService.GetRegion;

namespace Buddy.Application.CommandService.Group.Match
{
    public class MatchGroupCommandHandler : INotificationHandler<MatchGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _groupRepository;
        private readonly IMediator _mediator;


        public MatchGroupCommandHandler(IRepository<Domain.Entities.Group> groupRepository, IMediator mediator)
        {
            _groupRepository = groupRepository;
            _mediator = mediator;
        }

        public async Task Handle(MatchGroupCommand notification, CancellationToken cancellationToken)
        {
            var currentGroup = await _groupRepository.GetById(notification.GroupId);
            var region = await _mediator.Send(new GetRegionQuery(currentGroup.RegionId), cancellationToken);
            var otherGroups = await GetGroups(region.GroupIds);

            currentGroup.Match(otherGroups);
            await _groupRepository.Save(currentGroup);
        }

        private async Task<IList<Domain.Entities.Group>> GetGroups(IEnumerable<string> groupIds)
        {
            var tasks = await Task.WhenAll(groupIds.Select(id => _groupRepository.GetById(id)));
            return tasks.Where(task => task != null).ToList();
        }
    }
}
