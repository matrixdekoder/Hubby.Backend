using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupLeft: IEvent
    {
        public GroupLeft(string id, string groupId)
        {
            Id = id;
            GroupId = groupId;
        }
        public string Id { get; }
        public string GroupId { get; }
    }
}
