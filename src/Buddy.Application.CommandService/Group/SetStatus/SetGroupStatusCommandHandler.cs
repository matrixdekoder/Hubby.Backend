using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Group.SetStatus
{
    public class SetGroupStatusCommandHandler: INotificationHandler<SetGroupStatusCommand>
    {
        private readonly IRepository _repository;

        public SetGroupStatusCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SetGroupStatusCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById<Domain.Group>(notification.GroupId);
            group.SetStatus(notification.Status);
            await _repository.Save(group);
        }
    }
}
