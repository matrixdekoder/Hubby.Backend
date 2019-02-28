using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class MergeStarted: IEvent
    {
        public MergeStarted(string id, string matchedGroupId, List<string> blackListedIds)
        {
            Id = id;
            MatchedGroupId = matchedGroupId;
            BlackListedIds = blackListedIds;
        }

        public string Id { get; }
        public string MatchedGroupId { get; }
        public List<string> BlackListedIds { get; }
    }
}
