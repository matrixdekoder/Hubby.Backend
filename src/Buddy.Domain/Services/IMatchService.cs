using System.Threading.Tasks;
using Buddy.Domain.Entities;

namespace Buddy.Domain.Services
{
    public interface IMatchService
    {
        Task<Group> GetBestGroup(string buddyId);
    }
}
