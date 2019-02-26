using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Buddy.Application.CommandService.MergeGroup
{
    public class MergeGroupCommand: INotification
    {
        public MergeGroupCommand(string groupId, IList<string> otherGroupIds)
        {
            GroupId = groupId;
            OtherGroupIds = otherGroupIds;
        }
        public string GroupId { get; }
        public IList<string> OtherGroupIds { get; }
    }
}
