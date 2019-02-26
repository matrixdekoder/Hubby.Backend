using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Domain.Entities;

namespace Buddy.Domain.Services
{
    public interface IMatchService
    {
        Task<Group> GetBestGroup(Entities.Buddy buddy, IList<Group> groups);
        Task MergeGroups(Group currentGroup, Group matchedGroup);
    }
}
