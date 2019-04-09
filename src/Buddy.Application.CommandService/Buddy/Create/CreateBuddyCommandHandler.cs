using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.Create
{
    public class CreateBuddyCommandHandler: INotificationHandler<CreateBuddyCommand>
    {
        private readonly ISagaRepository _repository;

        public CreateBuddyCommandHandler(ISagaRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateBuddyCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.AccountId);
            buddy.Create(notification.AccountId);
            await _repository.Save(notification.TransactionId, buddy);
        }
    }
}
