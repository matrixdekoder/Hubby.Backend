using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupIsReset: IEvent
    {
        public GroupIsReset(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
