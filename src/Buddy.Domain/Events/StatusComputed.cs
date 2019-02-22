using Buddy.Domain.Enums;
using Core.Domain;

namespace Buddy.Domain.Events
{
    public class StatusComputed: IEvent
    {
        public StatusComputed(string id, BuddyStatus status)
        {
            Id = id;
            Status = status;
        }

        public string Id { get; }
        public BuddyStatus Status { get; }
    }
}
