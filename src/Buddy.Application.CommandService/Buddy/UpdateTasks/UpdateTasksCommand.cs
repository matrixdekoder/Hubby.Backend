using MediatR;

namespace Buddy.Application.CommandService.Buddy.UpdateTasks
{
    public class UpdateTasksCommand: INotification
    {
        public UpdateTasksCommand(string buddyId)
        {
            BuddyId = buddyId;
        }

        public string BuddyId { get; }
    }
}
