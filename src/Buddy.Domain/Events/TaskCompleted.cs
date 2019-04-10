using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class TaskCompleted: IEvent
    {
        public TaskCompleted(string id, Task task)
        {
            Id = id;
            Task = task;
        }

        public string Id { get; private set; }
        public Task Task { get; private set; }
    }
}
