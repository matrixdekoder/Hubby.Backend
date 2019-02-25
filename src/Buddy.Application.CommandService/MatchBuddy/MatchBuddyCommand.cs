using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.CommandService.MatchBuddy
{
    public class MatchBuddyCommand: INotification
    {
        public MatchBuddyCommand(string buddyId, List<string> groupIds)
        {
            BuddyId = buddyId;
            GroupIds = groupIds;
        }

        public string BuddyId { get; }
        public List<string> GroupIds { get; }
    }
}
