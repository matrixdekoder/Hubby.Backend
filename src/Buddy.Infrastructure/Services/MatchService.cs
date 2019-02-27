using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Domain.Entities;
using Buddy.Domain.Services;
using Library.EventStore;

namespace Buddy.Infrastructure.Services
{
    public class MatchService: IMatchService
    {
        private readonly IEventStoreRepository<Group> _groupRepository;
        private readonly IEventStoreRepository<Domain.Entities.Buddy> _buddyRepository;

        public MatchService(
            IEventStoreRepository<Group> groupRepository,
            IEventStoreRepository<Domain.Entities.Buddy> buddyRepository)
        {
            _groupRepository = groupRepository;
            _buddyRepository = buddyRepository;
        }

        public async Task<Group> GetBestGroup(Domain.Entities.Buddy buddy, IList<Group> groups)
        {
            var scoreByGroup = new Dictionary<string, double>();
            foreach (var group in groups)
            {
                var score = group.Match(buddy);
                if(Math.Abs(score) > 0.0)
                    scoreByGroup.Add(group.Id, score);
            }
            
            string matchedGroupId;
            Group matchedGroup;

            if (scoreByGroup.Any())
            {
                matchedGroupId = Guid.NewGuid().ToString();
                matchedGroup = await _groupRepository.GetById(matchedGroupId);
                matchedGroup.Start(matchedGroupId, buddy.RegionId, buddy.GenreIds.ToList());
            }
            else
            {
                matchedGroupId = scoreByGroup.OrderByDescending(x => x.Value).First().Key;
                matchedGroup = groups.First(x => x.Id == matchedGroupId);
            }

            return matchedGroup;
        }

        public async Task MergeGroups(Group currentGroup, Group matchedGroup, IList<string> otherGroupIds)
        {
            currentGroup.MergeBlacklist(matchedGroup);

            foreach (var mergedBuddyId in matchedGroup.BuddyIds)
            {
                var mergedBuddy = await _buddyRepository.GetById(mergedBuddyId);
                mergedBuddy.LeaveGroup(otherGroupIds);
                currentGroup.AddBuddy(mergedBuddy);
                mergedBuddy.JoinGroup(matchedGroup.Id);

                await _buddyRepository.Save(mergedBuddy);
            }

            matchedGroup.Clear();

            await _groupRepository.Save(currentGroup);
            await _groupRepository.Save(matchedGroup);
        }
    }
}
