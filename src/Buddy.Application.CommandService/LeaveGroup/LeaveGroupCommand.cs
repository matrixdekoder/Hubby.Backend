using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.CommandService.LeaveGroup
{
    public class LeaveGroupCommand: INotification
    {
        public LeaveGroupCommand(string buddyId, IList<string> groupIds)
        {
            BuddyId = buddyId;
            GroupIds = groupIds;
        }

        public string BuddyId { get; }
        public IList<string> GroupIds { get; }
    }
}
