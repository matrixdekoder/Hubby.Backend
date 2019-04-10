using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Entities;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.SetTaskStatus
{
    public class SetTaskStatusCommandHandler: INotificationHandler<SetTaskStatusCommand>
    {
        private readonly IRepository _repository;

        public SetTaskStatusCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(SetTaskStatusCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.BuddyId);
            buddy.SetTaskStatus(notification.TaskType, notification.NewStatus);
            await _repository.Save(buddy);
        }
    }
}
