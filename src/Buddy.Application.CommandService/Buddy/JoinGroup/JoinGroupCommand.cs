using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Buddy.Application.CommandService.Buddy.JoinGroup
{
    public class JoinGroupCommand: INotification
    {
        public JoinGroupCommand(string buddyId, string groupId)
        {
            BuddyId = buddyId;
            GroupId = groupId;
        }

        public string BuddyId { get; }
        public string GroupId { get; }
    }
}
