using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.StartMerge
{
    public class StartGroupMergeCommandHandler: INotificationHandler<StartGroupMergeCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _repository;

        public StartGroupMergeCommandHandler(IRepository<Domain.Entities.Group> repository)
        {
            _repository = repository;
        }

        public async Task Handle(StartGroupMergeCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(notification.GroupId);
            var matchedGroup = await _repository.GetById(notification.MatchedGroupId);

            group.StartMerge(matchedGroup);
            
            await _repository.Save(group);
            await _repository.Save(matchedGroup);
        }
    }
}
