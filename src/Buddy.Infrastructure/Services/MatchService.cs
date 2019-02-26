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

        public MatchService(IEventStoreRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public async Task<Group> GetBestGroup(Domain.Entities.Buddy buddy, IList<Group> groups)
        {
            var scoreByGroup = new Dictionary<string, double>();
            foreach (var group in groups)
            {
                if(buddy.PreviousGroupIds.Contains(group.Id))
                    continue;

                var score = group.Match(buddy);
                scoreByGroup.Add(group.Id, score);
            }
            
            string matchedGroupId;
            Group matchedGroup;

            if (scoreByGroup.Values.All(score => Math.Abs(score) <= 0.0))
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
    }
}
