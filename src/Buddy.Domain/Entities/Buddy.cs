using System;
using System.Collections.Generic;
using System.Linq;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Domain;

namespace Buddy.Domain.Entities
{
    public class Buddy: Aggregate<Buddy>
    {
        private const int GenresAmount = 5;
        private IList<string> _genreIds;
        private BuddyStatus _status;
        private string _currentGroupId;

        public Buddy(IEnumerable<IEvent> events) : base(events)
        {
        }
        
        public string RegionId { get; private set; }
        public IEnumerable<string> GenreIds => _genreIds.AsEnumerable();

        public void Create(string buddyId)
        {
            var e = new BuddyCreated(buddyId);
            Publish(e);
            ComputeStatus();
        }

        public void ChooseRegion(string regionId)
        {
            var e = new RegionChosen(Id, regionId);
            Publish(e);
            ComputeStatus();
        }

        public void ChooseGenres(IList<string> genreIds)
        {
            if(genreIds.Count != GenresAmount)
                throw new InvalidOperationException($"You have to pick exactly {GenresAmount} genres");

            var e = new GenresChosen(Id, genreIds);
            Publish(e);
            ComputeStatus();
        }

        public void JoinGroup(string groupId)
        {
            var e = new GroupJoined(Id, groupId);
            Publish(e);
        }

        private void ComputeStatus()
        {
            var status = BuddyStatus.New;

            if (RegionId != null && _genreIds != null && _genreIds.Count == 5)
                status = BuddyStatus.Complete;

            var e = new StatusComputed(Id, status);
            Publish(e);
        }

        private void When(BuddyCreated e)
        {
            Id = e.Id;
        }

        private void When(RegionChosen e)
        {
            RegionId = e.RegionId;
        }

        private void When(GenresChosen e)
        {
            _genreIds = e.GenreIds;
        }

        private void When(StatusComputed e)
        {
            _status = e.Status;
        }

        private void When(GroupJoined e)
        {
            _currentGroupId = e.GroupId;
        }
    }
}
