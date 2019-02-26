using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Application.QueryService.Group
{
    public class GroupReadModel: IEntity
    {
        public string Id { get; set; }
        public string RegionId { get; set; }
        public IList<string> GenreIds { get; set; }
        public IList<string> BuddyIds { get; set; }
    }
}
