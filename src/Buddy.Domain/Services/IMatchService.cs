using System.Threading.Tasks;

namespace Buddy.Domain.Services
{
    public interface IMatchService
    {
        Task<string> GetBestGroupId(Entities.Buddy buddyId);
    }
}
