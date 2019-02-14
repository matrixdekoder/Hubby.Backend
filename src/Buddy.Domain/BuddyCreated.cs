using Core.Domain;

namespace Buddy.Domain
{
    public class BuddyCreated: IEvent
    {
        public BuddyCreated(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
