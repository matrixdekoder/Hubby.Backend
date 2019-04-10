using Buddy.Domain.Events;
using System.Threading.Tasks;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Buddy.Listeners
{
    public class TaskStatusSetListener : QueryListener<TaskStatusSet>
    {
        public TaskStatusSetListener(IProjectionWriter writer) : base(writer)
        {
        }

        protected override async Task Handle(TaskStatusSet notification)
        {
            await Writer.Update<BuddyReadModel>(notification.Id, view =>
            {
                var index = view.Tasks.FindIndex(x => x.Type == notification.Type);
                view.Tasks[index].SetStatus(notification.NewStatus);
            });
        }
    }
}
