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
        #region Fields

        private const int GenresAmount = 5;
        private IList<string> _genreIds;
        private List<string> _previousGroups;

        #endregion

        #region Constructor

        public Buddy(IEnumerable<IEvent> events) : base(events)
        {
        }

        #endregion

        #region Properties

        public string RegionId { get; private set; }
        public IEnumerable<string> GenreIds => _genreIds.AsEnumerable();
        public BuddyStatus Status { get; private set; }
        public string CurrentGroupId { get; private set; }
        public IEnumerable<string> PreviousGroupIds => _previousGroups.AsEnumerable();

        #endregion

        #region Public Methods

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
            if (genreIds.Count != GenresAmount)
                throw new InvalidOperationException($"You have to pick exactly {GenresAmount} genres");

            var e = new GenresChosen(Id, genreIds);
            Publish(e);
            ComputeStatus();
        }

        public void JoinGroup(string groupId)
        {
            if (_previousGroups.Contains(groupId))
                throw new InvalidOperationException("Already been in this group");

            var e = new GroupJoined(Id, groupId);
            Publish(e);
        }

        public void LeaveGroup()
        {
            if (CurrentGroupId == null)
                throw new InvalidOperationException($"Buddy {Id} isn't in a group yet");

            var e = new GroupLeft(Id);
            Publish(e);
        }

        #endregion

        #region Private Methods: Events

        private void When(BuddyCreated e)
        {
            Id = e.Id;
            _previousGroups = new List<string>();
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
            Status = e.Status;
        }

        private void When(GroupJoined e)
        {
            CurrentGroupId = e.GroupId;
        }

        private void When(GroupLeft e)
        {
            _previousGroups.Add(CurrentGroupId);
            CurrentGroupId = null;
        }


        #endregion

        #region Private Methods

        private void ComputeStatus()
        {
            var status = BuddyStatus.New;

            if (RegionId != null && _genreIds != null && _genreIds.Count == 5)
                status = BuddyStatus.Complete;

            var e = new StatusComputed(Id, status);
            Publish(e);
        }

        #endregion
    }
}
