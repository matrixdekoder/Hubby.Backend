using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BuddyGroupCreated: IEvent
    {
        public BuddyGroupCreated(string id, string groupId, string regionId, IList<string> genreIds)
        {
            Id = id;
            GroupId = groupId;
            RegionId = regionId;
            GenreIds = genreIds;
        }

        public string Id { get; }
        public string GroupId { get; }
        public string RegionId { get; }
        public IList<string> GenreIds { get; }
    }
}
