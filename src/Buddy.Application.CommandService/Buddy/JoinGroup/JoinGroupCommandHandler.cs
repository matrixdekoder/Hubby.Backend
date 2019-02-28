using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class JoinGroupCommandHandler: INotificationHandler<JoinGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Buddy> _repository;

        public JoinGroupCommandHandler(IRepository<Domain.Entities.Buddy> repository)
        {
            _repository = repository;
        }

        public async Task Handle(JoinGroupCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById(notification.BuddyId);
            buddy.JoinGroup(notification.GroupId);
            await _repository.Save(buddy);
        }
    }
}
