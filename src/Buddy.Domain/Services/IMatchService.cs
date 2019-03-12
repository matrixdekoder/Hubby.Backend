using System.Threading.Tasks;

namespace Buddy.Domain.Services
{
    public interface IMatchService
    {
        Task<Group> GetBestGroup(Buddy buddy);
    }
}
