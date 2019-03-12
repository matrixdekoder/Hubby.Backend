using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddy
{
    class RemoveBuddyCommandHandler: INotificationHandler<RemoveBuddyCommand>
    {
        private readonly IRepository _repository;

        public RemoveBuddyCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RemoveBuddyCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById<Domain.Entities.Group>(notification.GroupId);
            group.RemoveBuddy(notification.BuddyId);
            await _repository.Save(group);
        }
    }
}
