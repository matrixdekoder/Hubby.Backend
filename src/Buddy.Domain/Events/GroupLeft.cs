using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupLeft: IEvent
    {
        public GroupLeft(string id, string groupId, IList<string> groupIds)
        {
            Id = id;
            GroupId = groupId;
            GroupIds = groupIds;
        }
        public string Id { get; }
        public string GroupId { get; }
        public IList<string> GroupIds { get; }
    }
}
