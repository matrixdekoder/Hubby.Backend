using System;
using System.Collections.Generic;
using System.Linq;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Domain;

namespace Buddy.Domain
{
    public class Group : Aggregate<Group>
    {
        #region Fields

        private const int MaximumGroupSize = 6;
        private const double MinimumGenreMatchWeight = 0.6;

        private List<string> _genreIds;
        private List<string> _buddyIds;
        private List<string> _buddyIdsBlackList;

        #endregion

        #region Properties

        public int CurrentGroupSize => _buddyIds.Count;
        public int FreeSpace => MaximumGroupSize - CurrentGroupSize;
        public string RegionId { get; private set; }
        public IEnumerable<string> GenreIds => _genreIds.AsEnumerable();
        public IEnumerable<string> BuddyIds => _buddyIds.AsEnumerable();
        public IEnumerable<string> Blacklist => _buddyIdsBlackList.AsEnumerable();
        public GroupStatus Status { get; private set; }

        #endregion

        #region Public Methods

        public void Create(string groupId, string regionId, IList<string> genreIds)
        {
            if (Version > 0)
                throw new InvalidOperationException("Can't create a group twice");

            var e = new GroupCreated(groupId, regionId, genreIds);
            Publish(e);
        }

        public void AddBuddy(Buddy buddy, BuddyJoinType joinType)
        {
            if (_buddyIdsBlackList.Contains(buddy.Id))
                throw new InvalidOperationException("Buddy is on this group's blacklist");

            if (_buddyIds.Contains(buddy.Id))
                throw new InvalidOperationException($"Buddy {buddy.Id} is already in the group");

            if (_buddyIds.Count >= MaximumGroupSize)
                throw new InvalidOperationException($"Only {MaximumGroupSize} buddies are allowed per group");

            var e = new BuddyAdded(Id, buddy.Id, joinType);
            Publish(e);
        }

        public void RemoveBuddy(string buddyId)
        {
            if (!_buddyIds.Any())
                throw new InvalidOperationException("Group is already empty");

            if(Status != GroupStatus.Merging && _buddyIds.Count == 1)
                throw new InvalidOperationException("Can't leave group when you're the only one in it");

            if (!_buddyIds.Contains(buddyId))
                throw new InvalidOperationException($"Buddy {buddyId} isn't present in the current group");

            var e = new BuddyRemoved(Id, buddyId);
            Publish(e);
        }

        public double GetScore(Buddy buddy)
        {
            if(Status == GroupStatus.Merging)
                throw new InvalidOperationException("Can't match while merging");

            if (buddy.RegionId != RegionId)
                throw new InvalidOperationException("Group region different from group's region");

            if (CurrentGroupSize >= MaximumGroupSize)
                return 0.0;

            if (_buddyIdsBlackList.Contains(buddy.Id))
                return 0.0;

            // Calculate match based on genre
            var genresAmount = buddy.GenreIds.Count();
            var delta = _genreIds.Except(buddy.GenreIds).Count();
            var genreMatchWeight = (double)(genresAmount - delta) / genresAmount;

            if (genreMatchWeight < MinimumGenreMatchWeight)
                return 0.0;

            // Prioritize fuller groups over emptier ones
            return genreMatchWeight / (MaximumGroupSize - CurrentGroupSize);
        }

        public void Match(IList<Group> otherGroups)
        {
            if (Status == GroupStatus.Merging)
                return;

            var matchedGroups = otherGroups
                .Where(x => x.Id != Id)
                .Where(HasSameGenres)
                .Where(HasFreeSpace)
                .Where(BuddiesAllowed)
                .Where(x => x.Status != GroupStatus.Merging)
                .ToList();

            var matchedGroup = matchedGroups.OrderByDescending(x => x.FreeSpace).FirstOrDefault();
            if (matchedGroup == null) return;

            var e = new GroupMatched(Id, matchedGroup.Id);
            Publish(e);
        }

        public void SetStatus(GroupStatus status)
        {
            Publish(new GroupStatusSet(Id, status));
        }

        public void StartMerge(Group otherGroup)
        {
            if(Id == otherGroup.Id)
                throw new InvalidOperationException("Can't merge the 2 same groups");

            var e = new MergeStarted(Id, otherGroup.Id, otherGroup.Blacklist.ToList());
            Publish(e);
        }

        public void MergeBuddies(Group otherGroup, IEnumerable<Buddy> buddiesToMerge, long matchedGroupTransaction)
        {
            if (Status != GroupStatus.Merging || otherGroup.Status != GroupStatus.Merging)
                throw new InvalidOperationException("Both groups must be in merging status to merge");

            if (MaximumGroupSize - CurrentGroupSize < otherGroup.CurrentGroupSize)
                throw new InvalidOperationException("Can't merge groups, not enough space");

            if(!BuddiesAllowed(otherGroup))
                throw new InvalidOperationException("Some of the other groups buddies are blacklisted, unable to merge groups");

            foreach (var buddy in buddiesToMerge)
            {
                AddBuddy(buddy, BuddyJoinType.Merge);
            }

            var e = new BuddiesMerged(Id, otherGroup.Id, matchedGroupTransaction);
            Publish(e);
        }

        public void Clear()
        {
            Publish(new GroupIsCleared(Id));
        }

        #endregion

        #region Private Methods: Events

        private void When(GroupCreated e)
        {
            Id = e.Id;
            RegionId = e.RegionId;
            _genreIds = e.GenreIds.ToList();
            _buddyIds = new List<string>();
            _buddyIdsBlackList = new List<string>();
            Status = GroupStatus.Open;
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

        private void When(MergeStarted e)
        {
            var otherBlacklistIds = e.BlackListedIds.Except(_buddyIdsBlackList);
            _buddyIdsBlackList.AddRange(otherBlacklistIds);
            Status = GroupStatus.Merging;
        }

        private void When(GroupStatusSet e)
        {
            Status = e.Status;
        }

        private void When(GroupIsCleared e)
        {
            _buddyIdsBlackList.Clear();
            _buddyIds.Clear();
            Status = GroupStatus.Open;
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
            // Check if buddies in other groups are allowed
            if (_buddyIdsBlackList.Except(otherGroup.BuddyIds).Count() != _buddyIdsBlackList.Count)
                return false;

            // Check if buddies of this group are allowed to match with other group
            return otherGroup.Blacklist.Except(_buddyIds).Count() == otherGroup.Blacklist.Count();
        }

        #endregion
    }
}
