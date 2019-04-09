using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class JoinGroupCommandHandler: INotificationHandler<JoinGroupCommand>
    {
        private readonly ISagaRepository _repository;

        public JoinGroupCommandHandler(ISagaRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(JoinGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.BuddyId);
            buddy.JoinGroup(notification.GroupId, notification.Type);
            await _repository.Save(notification.TransactionId, buddy);
        }
    }
}
