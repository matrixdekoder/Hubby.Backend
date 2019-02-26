﻿using System;
using System.Collections.Generic;
using System.Linq;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Domain;

namespace Buddy.Domain.Entities
{
    public class Group: Aggregate<Group>
    {
        private const int MaximumGroupSize = 6;
        private const double MinimumGenreMatchWeight = 0.6;

        private string _regionId;
        private IList<string> _genreIds;
        private IList<string> _buddyIds;

        public Group(IEnumerable<IEvent> events) : base(events)
        {
        }

        private int CurrentGroupSize => _buddyIds.Count;

        public void Start(string groupId, string regionId, IList<string> genreIds)
        {
            if(Version > 0)
                throw new InvalidOperationException("Can't start a group twice");

            var e = new GroupStarted(groupId, regionId, genreIds);
            Publish(e);
        }

        public void AddBuddy(string buddyId)
        {
            if(_buddyIds.Contains(buddyId))
                throw new InvalidOperationException($"Buddy {buddyId} is already in the group");

            if(_buddyIds.Count >= MaximumGroupSize)
                throw new InvalidOperationException($"Only {MaximumGroupSize} buddies are allowed per group");

            var e = new BuddyAdded(Id, buddyId);
            Publish(e);
        }

        public double Match(Buddy buddy)
        {
            if(buddy.RegionId != _regionId)
                throw new InvalidOperationException("Group region different from group's region");

            if (CurrentGroupSize >= MaximumGroupSize)
                return 0.0;

            if(buddy.Status != BuddyStatus.Complete)
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

        private void When(GroupStarted e)
        {
            Id = e.Id;
            _regionId = e.RegionId;
            _genreIds = e.GenreIds;
            _buddyIds = new List<string>();
        }

        private void When(BuddyAdded e)
        {
            _buddyIds.Add(e.BuddyId);
        }
    }
}