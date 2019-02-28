using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Enums;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.Merge
{
    public class MergeCommandHandler: INotificationHandler<MergeGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _repository;

        public MergeCommandHandler(IRepository<Domain.Entities.Group> repository)
        {
            _repository = repository;
        }

        public async Task Handle(MergeGroupCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(notification.GroupId);
            var matchedGroup = await _repository.GetById(notification.MatchedGroupId);

            group.Merge(matchedGroup);
            matchedGroup.SetStatus(GroupStatus.Merging);

            await _repository.Save(group);
            await _repository.Save(matchedGroup);
        }
    }
}
