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
        private readonly ISagaRepository _repository;
        private readonly IMediator _mediator;


        public MatchGroupCommandHandler(ISagaRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(MatchGroupCommand notification, CancellationToken cancellationToken)
        {
            var currentGroup = await _repository.GetById<Domain.Group>(notification.GroupId);
            var region = await _mediator.Send(new GetRegionQuery(currentGroup.RegionId), cancellationToken);
            var otherGroups = await GetGroups(region.GroupIds);

            currentGroup.Match(otherGroups);
            await _repository.Save(notification.TransactionId, currentGroup);
        }

        private async Task<IList<Domain.Group>> GetGroups(IEnumerable<string> groupIds)
        {
            var tasks = await Task.WhenAll(groupIds.Select(id => _repository.GetById<Domain.Group>(id)));
            return tasks.Where(task => task != null).ToList();
        }
    }
}
