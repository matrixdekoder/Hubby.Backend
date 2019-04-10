using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Group.Clear
{
    public class ClearGroupCommandHandler: INotificationHandler<ClearGroupCommand>
    {
        private readonly IRepository _repository;

        public ClearGroupCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ClearGroupCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById<Domain.Group>(notification.GroupId);
            group.Clear();
            await _repository.Save(group);
        }
    }
}
