using System.Collections.Generic;
using System.Threading.Tasks;

namespace Buddy.Domain.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Task>> GetBuddyTasks();
    }
}
