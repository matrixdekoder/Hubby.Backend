using Buddy.Domain.Enums;

namespace Buddy.Domain
{
    public class Task
    {
        public Task(string type)
        {
            Type = type;
            Status = TaskStatus.Open;
        }

        public string Type { get; private set; }
        public TaskStatus Status { get; private set; }

        public void SetStatus(TaskStatus status)
        {
            Status = status;
        }
    }
}
