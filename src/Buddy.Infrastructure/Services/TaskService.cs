using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Domain.Constants;
using Buddy.Domain.Services;

namespace Buddy.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        public async Task<IEnumerable<Domain.Task>> GetBuddyTasks()
        {
            return await Task.FromResult(GetTasks());
        }

        private static ICollection<Domain.Task> GetTasks()
        {
            return new List<Domain.Task>
            {
                new Domain.Task(TaskType.ChooseRegion),
                new Domain.Task(TaskType.ChooseGenres)
            };
        }
    }
}
