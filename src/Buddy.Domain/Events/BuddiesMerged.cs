using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BuddiesMerged: IEvent
    {
        public BuddiesMerged(string id, string matchedGroupId, long matchedGroupTransaction)
        {
            Id = id;
            MatchedGroupId = matchedGroupId;
            MatchedGroupTransaction = matchedGroupTransaction;
        }

        public string Id { get; }
        public string MatchedGroupId { get; }
        public long MatchedGroupTransaction { get; }
    }
}
