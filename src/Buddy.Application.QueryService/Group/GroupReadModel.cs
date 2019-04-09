using System.Collections.Generic;
using Core.Application;
using Core.Application.Query;

namespace Buddy.Application.QueryService.Group
{
    public class GroupReadModel: IReadModel
    {
        public string Id { get; set; }
        public string RegionId { get; set; }
        public IList<string> GenreIds { get; set; }
        public IList<string> BuddyIds { get; set; }
    }
}
