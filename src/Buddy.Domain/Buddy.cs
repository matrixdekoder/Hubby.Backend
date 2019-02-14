using System.Collections.Generic;
using Core.Domain;

namespace Buddy.Domain
{
    public class Buddy: Aggregate<Buddy>
    {
        private string _regionId;

        public Buddy(IEnumerable<IEvent> events) : base(events)
        {
        }

        public void Create(string buddyId)
        {
            var e = new BuddyCreated(buddyId);
            Publish(e);
        }

        public void ChooseRegion(string regionId)
        {
            var e = new RegionChosen(Id, regionId);
            Publish(e);
        }

        private void When(BuddyCreated e)
        {
            Id = e.Id;
        }

        private void When(RegionChosen e)
        {
            _regionId = e.RegionId;
        }
    }
}
