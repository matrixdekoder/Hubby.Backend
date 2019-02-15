using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Application.QueryService.Buddy
{
    public class BuddyReadModel: IEntity
    {
        public string Id { get; set; }
        public string RegionId { get; set; }
        public IList<string> GenreIds { get; set; }
    }
}
