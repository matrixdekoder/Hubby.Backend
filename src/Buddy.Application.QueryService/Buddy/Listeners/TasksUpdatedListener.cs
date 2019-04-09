using Buddy.Domain.Events;
using Core.Application;
using System.Threading.Tasks;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class TasksUpdatedListener : QueryListener<TasksUpdated>
    {
        public TasksUpdatedListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(TasksUpdated notification)
        {
            await Writer.Update<BuddyReadModel>(notification.Id, view =>
            {
                foreach (var task in notification.Tasks)
                {
                    if (view.Tasks.Contains(task)) continue;
                    view.Tasks.Add(task);
                }
            });
        }
    }
}
