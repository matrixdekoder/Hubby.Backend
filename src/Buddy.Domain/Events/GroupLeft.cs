using Core.Domain;

namespace Buddy.Domain.Events
{
    public class GroupLeft: IEvent
    {
        public GroupLeft(string id)
        {
            Id = id;
        }
        public string Id { get; }
    }
}
