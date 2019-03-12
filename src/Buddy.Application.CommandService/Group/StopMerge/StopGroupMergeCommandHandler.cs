using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.StopMerge
{
    public class StopGroupMergeCommandHandler: INotificationHandler<StopGroupMergeCommand>
    {
        private readonly IRepository _repository;

        public StopGroupMergeCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(StopGroupMergeCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById<Domain.Group>(notification.GroupId);
            var matchedGroup = await _repository.GetById<Domain.Group>(notification.MatchedGroupId);

            group.StopMerge(matchedGroup);

            await _repository.Save(matchedGroup);
            await _repository.Save(group);
        }
    }
}
