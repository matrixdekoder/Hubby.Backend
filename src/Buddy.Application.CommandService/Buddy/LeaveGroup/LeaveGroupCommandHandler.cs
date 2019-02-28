using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class LeaveGroupCommandHandler: INotificationHandler<LeaveGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Buddy> _repository;

        public LeaveGroupCommandHandler(IRepository<Domain.Entities.Buddy> repository)
        {
            _repository = repository;
        }

        public async Task Handle(LeaveGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById(notification.BuddyId);
            buddy.LeaveGroup();
            await _repository.Save(buddy);
        }
    }
}
