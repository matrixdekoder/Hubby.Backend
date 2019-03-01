using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Account.Application.CommandService.LinkBuddy
{
    public class LinkBuddyCommand: INotification
    {
        public LinkBuddyCommand(string accountId, string buddyId)
        {
            AccountId = accountId;
            BuddyId = buddyId;
        }

        public string AccountId { get; }
        public string BuddyId { get; }
    }
}
