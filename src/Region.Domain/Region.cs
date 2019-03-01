using System;
using System.Collections.Generic;
using System.Linq;
using Core.Domain;

namespace Region.Domain
{
    public class Region: Aggregate<Region>
    {
        private string _name;
        private IList<string> _groupIds;

        public Region(IEnumerable<IEvent> events) : base(events)
        {
        }

        public void Create(string id, string name)
        {
            var e = new RegionCreated(id, name);
            Publish(e);
        }

        public IEnumerable<string> GroupIds => _groupIds.AsEnumerable();

        public void AddGroup(string groupId)
        {
            if(Version == 0)
                throw new InvalidOperationException("Group needs to be started first");

            if(_groupIds.Contains(groupId))
                throw new InvalidOperationException("Group already added to region");

            var e = new GroupAddedToRegion(Id, groupId);
            Publish(e);
        }

        private void When(RegionCreated e)
        {
            Id = e.Id;
            _name = e.Name;
            _groupIds = new List<string>();
        }

        private void When(GroupAddedToRegion e)
        {
            _groupIds.Add(e.GroupId);
        }
    }
}
