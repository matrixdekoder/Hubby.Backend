using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class LeaveGroupCommandHandler: INotificationHandler<LeaveGroupCommand>
    {
        private readonly ISagaRepository _repository;

        public LeaveGroupCommandHandler(ISagaRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(LeaveGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.BuddyId);
            buddy.LeaveGroup();
            await _repository.Save(notification.TransactionId, buddy);
        }
    }
}
