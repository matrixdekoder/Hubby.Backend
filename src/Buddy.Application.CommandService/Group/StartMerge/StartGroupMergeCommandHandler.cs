using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Group.StartMerge
{
    public class StartGroupMergeCommandHandler: INotificationHandler<StartGroupMergeCommand>
    {
        private readonly IRepository _repository;

        public StartGroupMergeCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(StartGroupMergeCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById<Domain.Group>(notification.GroupId);
            var matchedGroup = await _repository.GetById<Domain.Group>(notification.MatchedGroupId);
            group.StartMerge(matchedGroup);
            await _repository.Save(group);
        }
    }
}
