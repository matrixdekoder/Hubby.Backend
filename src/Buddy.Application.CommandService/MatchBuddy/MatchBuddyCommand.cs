using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.CommandService.MatchBuddy
{
    public class MatchBuddyCommand: INotification
    {
        public MatchBuddyCommand(string buddyId, IList<string> groupIds)
        {
            BuddyId = buddyId;
            GroupIds = groupIds;
        }

        public string BuddyId { get; }
        public IList<string> GroupIds { get; }
    }
}
