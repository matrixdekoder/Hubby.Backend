using Core.Domain;
using Core.Domain.Events;

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
