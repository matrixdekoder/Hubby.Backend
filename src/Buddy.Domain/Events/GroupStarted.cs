using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupStarted: IEvent
    {
        public GroupStarted(string id, string regionId, IList<string> genreIds)
        {
            Id = id;
            RegionId = regionId;
            GenreIds = genreIds;
        }
        public string Id { get; }
        public string RegionId { get; }
        public IList<string> GenreIds { get; }
    }
}
