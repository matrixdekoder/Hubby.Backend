using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupMatched: IEvent
    {
        public GroupMatched(string id, string matchId)
        {
            Id = id;
            MatchId = matchId;
        }

        public string Id { get; }
        public string MatchId { get; }
    }
}
