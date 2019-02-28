using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.AddBuddy
{
    public class AddBuddyCommandHandler: INotificationHandler<AddBuddyCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _repository;

        public AddBuddyCommandHandler(IRepository<Domain.Entities.Group> repository)
        {
            _repository = repository;
        }

        public async Task Handle(AddBuddyCommand notification, CancellationToken cancellationToken)
        {
            var group = await _repository.GetById(notification.GroupId);
            group.AddBuddy(notification.BuddyId);
            await _repository.Save(group);
        }
    }
}
