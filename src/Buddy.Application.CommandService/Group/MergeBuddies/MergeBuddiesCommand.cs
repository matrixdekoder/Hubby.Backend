using System.Collections.Generic;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.MergeBuddies
{
    public class MergeBuddiesCommand: INotification
    {
        public MergeBuddiesCommand(string groupId, string matchedGroupId, long transactionId, IEnumerable<IEvent> events, long matchedGroupTransaction)
        {
            GroupId = groupId;
            MatchedGroupId = matchedGroupId;
            TransactionId = transactionId;
            Events = events;
            MatchedGroupTransaction = matchedGroupTransaction;
        }

        public string GroupId { get; }
        public string MatchedGroupId { get; }
        public long TransactionId { get; }
        public IEnumerable<IEvent> Events { get; }
        public long MatchedGroupTransaction { get; }
    }
}
