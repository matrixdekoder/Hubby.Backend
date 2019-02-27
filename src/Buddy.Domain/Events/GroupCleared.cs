using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupCleared: IEvent
    {
        public GroupCleared(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
