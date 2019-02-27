using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BuddyRemoved: IEvent
    {
        public BuddyRemoved(string id, string buddyId, IList<string> groupIds)
        {
            Id = id;
            BuddyId = buddyId;
            GroupIds = groupIds;
        }
        public string Id { get; }
        public string BuddyId { get; }
        public IList<string> GroupIds { get; }
    }
}
