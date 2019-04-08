using Buddy.Domain.Services;
using Core.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Buddy.Application.CommandService.Buddy.UpdateTasks
{
    public class UpdateTasksCommandHandler : INotificationHandler<UpdateTasksCommand>
    {
        private readonly IRepository _repository;
        private readonly ITaskService _taskService;

        public UpdateTasksCommandHandler(IRepository repository, ITaskService taskService)
        {
            _repository = repository;
            _taskService = taskService;
        }

        public async Task Handle(UpdateTasksCommand notification, CancellationToken cancellationToken)
        {
            var buddy = await _repository.GetById<Domain.Buddy>(notification.BuddyId);
            var tasks = await _taskService.GetBuddyTasks();
            buddy.UpdateTasks(tasks);
            await _repository.Save(buddy);
        }
    }
}
