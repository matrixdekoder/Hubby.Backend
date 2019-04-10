using Buddy.Domain.Enums;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.SetTaskStatus
{
    public class SetTaskStatusCommand : INotification
    {
        public SetTaskStatusCommand(string buddyId, string taskType, TaskStatus newStatus)
        {
            BuddyId = buddyId;
            TaskType = taskType;
            NewStatus = newStatus;
        }

        public string BuddyId { get; }
        public string TaskType { get; }
        public TaskStatus NewStatus { get; }
    }
}
