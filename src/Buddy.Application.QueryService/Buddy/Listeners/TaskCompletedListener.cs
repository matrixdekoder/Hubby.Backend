using Buddy.Domain.Events;
using Core.Application;
using System.Threading.Tasks;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class TaskCompletedListener : QueryListener<TaskCompleted>
    {
        public TaskCompletedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(TaskCompleted notification)
        {
            await Writer.Update<BuddyReadModel>(notification.Id, view =>
            {
                var index = view.Tasks.FindIndex(x => x.Type == notification.Task.Type);
                view.Tasks[index] = notification.Task;
            });
        }
    }
}
