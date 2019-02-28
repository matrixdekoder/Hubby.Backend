using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddy
{
    class RemoveBuddyCommandHandler: INotificationHandler<RemoveBuddyCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _repository;

        public RemoveBuddyCommandHandler(IRepository<Domain.Entities.Group> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBuddyCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(notification.GroupId);
            group.RemoveBuddy(notification.BuddyId);
            await _repository.Save(group);
        }
    }
}
