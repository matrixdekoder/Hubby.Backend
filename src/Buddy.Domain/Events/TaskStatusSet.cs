using Buddy.Domain.Enums;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class TaskStatusSet: IEvent
    {
        public TaskStatusSet(string id, string type, TaskStatus newStatus)
        {
            Id = id;
            Type = type;
            NewStatus = newStatus;
        }

        public string Id { get; }
        public string Type { get; }
        public TaskStatus NewStatus { get; }
    }
}
