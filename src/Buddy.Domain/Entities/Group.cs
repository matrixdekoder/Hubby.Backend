using System;
using System.Collections.Generic;
using System.Linq;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Domain;

namespace Buddy.Domain.Entities
{
    public class Group : Aggregate<Group>
    {
        #region Fields

        private const int MaximumGroupSize = 6;
        private const double MinimumGenreMatchWeight = 0.6;

        private string _regionId;
        private IList<string> _genreIds;
        private IList<string> _buddyIds;
        private IList<string> _buddyIdsBlackList;

        #endregion

        #region Constructor

        public Group(IEnumerable<IEvent> events) : base(events)
        {
        }

        #endregion

        #region Properties

        public int CurrentGroupSize => _buddyIds.Count;
        public int FreeSpace => MaximumGroupSize - CurrentGroupSize;
        public IEnumerable<string> GenreIds => _genreIds.AsEnumerable();
        public IEnumerable<string> BuddyIds => _buddyIds.AsEnumerable();


        #endregion

        #region Public Methods

        public void Start(string groupId, string regionId, IList<string> genreIds)
        {
            if (Version > 0)
                throw new InvalidOperationException("Can't start a group twice");

            var e = new GroupStarted(groupId, regionId, genreIds);
            Publish(e);
        }

        public void AddBuddy(string buddyId)
        {
            if (_buddyIds.Contains(buddyId))
                throw new InvalidOperationException($"Buddy {buddyId} is already in the group");

            if (_buddyIds.Count >= MaximumGroupSize)
                throw new InvalidOperationException($"Only {MaximumGroupSize} buddies are allowed per group");

            var e = new BuddyAdded(Id, buddyId);
            Publish(e);
        }

        public double Match(Buddy buddy)
        {
            if (buddy.RegionId != _regionId)
                throw new InvalidOperationException("Group region different from group's region");

            if (CurrentGroupSize >= MaximumGroupSize)
                return 0.0;

            if (buddy.Status != BuddyStatus.Complete)
                throw new InvalidOperationException("Buddy not activated yet, please complete the basic setup");

            // Calculate match based on genre
            var genresAmount = buddy.GenreIds.Count();
            var delta = _genreIds.Except(buddy.GenreIds).Count();
            var genreMatchWeight = (double)(genresAmount - delta) / genresAmount;

            if (genreMatchWeight < MinimumGenreMatchWeight)
                return 0.0;

            // Prioritize fuller groups over emptier ones
            return genreMatchWeight / (MaximumGroupSize - CurrentGroupSize);
        }

        public Group Match(IList<Group> otherGroups)
        {
            var matchedGroups = otherGroups
                .Where(HasSameGenres)
                .Where(HasFreeSpace)
                .Where(BuddiesAllowed)
                .ToList();

            return matchedGroups.OrderByDescending(x => x.FreeSpace).FirstOrDefault();
        }

        public void RemoveBuddy(string buddyId)
        {
            if (!_buddyIds.Any())
                throw new InvalidOperationException("Group is already empty");

            if (!_buddyIds.Contains(buddyId))
                throw new InvalidOperationException($"Buddy {buddyId} isn't present in the current group");

            var e = new BuddyRemoved(Id, buddyId);
            Publish(e);
        }

        #endregion

        #region Private Methods: Events

        private void When(GroupStarted e)
        {
            Id = e.Id;
            _regionId = e.RegionId;
            _genreIds = e.GenreIds;
            _buddyIds = new List<string>();
            _buddyIdsBlackList = new List<string>();
        }

        private void When(BuddyAdded e)
        {
            _buddyIds.Add(e.BuddyId);
        }

        private void When(BuddyRemoved e)
        {
            _buddyIds.Remove(e.BuddyId);
            _buddyIdsBlackList.Add(e.BuddyId);
        }

        #endregion

        #region Private Methods

        private bool HasFreeSpace(Group otherGroup)
        {
            return CurrentGroupSize <= otherGroup.FreeSpace;
        }

        private bool HasSameGenres(Group otherGroup)
        {
            var currentGroupGenres = _genreIds.ToList();
            var otherGroupGenres = otherGroup.GenreIds.ToList();
            return !currentGroupGenres.Except(otherGroupGenres).Any() && !otherGroupGenres.Except(currentGroupGenres).Any();
        }

        private bool BuddiesAllowed(Group otherGroup)
        {
            return otherGroup.BuddyIds.Except(_buddyIdsBlackList).Count() == otherGroup.BuddyIds.Count();
        }

        #endregion
    }
}
