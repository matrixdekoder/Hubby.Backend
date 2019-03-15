using Core.Domain;

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
