using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.SetStatus
{
    public class SetGroupStatusCommandHandler: INotificationHandler<SetGroupStatusCommand>
    {
        private readonly ISagaRepository _repository;

        public SetGroupStatusCommandHandler(ISagaRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SetGroupStatusCommand notification, CancellationToken cancellationToken)
        {
            var transactionId = await _repository.StartTransaction<Domain.Group>(notification.GroupId);
            var group = await _repository.GetById<Domain.Group>(notification.GroupId);
            group.SetStatus(notification.Status);
            await _repository.Save(transactionId, group);
        }
    }
}
