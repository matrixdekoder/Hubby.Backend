using System.Collections.Generic;
using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class GroupCreated: IEvent
    {
        public GroupCreated(string id, string regionId, IList<string> genreIds)
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
