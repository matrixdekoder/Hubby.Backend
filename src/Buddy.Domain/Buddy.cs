using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain
{
    public class Buddy: Aggregate<Buddy>
    {
        public Buddy(IEnumerable<IEvent> events) : base(events)
        {
        }

        public void Create(string buddyId)
        {
            var e = new BuddyCreated(buddyId);
            Publish(e);
        }

        private void When(BuddyCreated e)
        {
            Id = e.Id;
        }
    }
}
