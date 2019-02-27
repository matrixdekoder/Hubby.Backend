using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.CommandService.RemoveBuddy
{
    public class RemoveBuddyCommand: INotification
    {
        public RemoveBuddyCommand(string buddyId, string groupId, IList<string> groupIds)
        {
            BuddyId = buddyId;
            GroupId = groupId;
            GroupIds = groupIds;
        }

        public string BuddyId { get; }
        public string GroupId { get; }
        public IList<string> GroupIds { get; }
    }
}
