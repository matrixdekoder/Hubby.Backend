using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class BlacklistMerged: IEvent
    {
        public BlacklistMerged(string id, List<string> blackListedIds)
        {
            Id = id;
            BlackListedIds = blackListedIds;
        }

        public string Id { get; }
        public List<string> BlackListedIds { get; }
    }
}
