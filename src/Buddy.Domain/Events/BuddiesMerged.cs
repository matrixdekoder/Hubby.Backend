using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BuddiesMerged: IEvent
    {
        public BuddiesMerged(string id, string matchedGroupId)
        {
            Id = id;
            MatchedGroupId = matchedGroupId;
        }

        public string Id { get; }
        public string MatchedGroupId { get; }
    }
}
