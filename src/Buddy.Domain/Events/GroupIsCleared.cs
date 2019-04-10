using Core.Domain;
using Core.Domain.Events;

namespace Buddy.Domain.Events
{
    public class GroupIsCleared: IEvent
    {
        public GroupIsCleared(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
