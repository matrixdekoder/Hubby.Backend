using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.LeaveGroup
{
    public class LeaveGroupCommandHandler: INotificationHandler<LeaveGroupCommand>
    {
        private readonly IRepository _repository;

        public LeaveGroupCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(LeaveGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.BuddyId);
            buddy.LeaveGroup();
            await _repository.Save(buddy);
        }
    }
}
