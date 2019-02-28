using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Domain.Services;
using Core.Domain;

namespace Buddy.Infrastructure.Services
{
    public class MatchService: IMatchService
    {
        private readonly IRepository<Domain.Entities.Group> _groupRepository;
        private readonly IRepository<Domain.Entities.Region> _regionRepository;

        public MatchService(
            IRepository<Domain.Entities.Group> groupRepository,
            IRepository<Domain.Entities.Region> regionRepository)
        {
            _groupRepository = groupRepository;
            _regionRepository = regionRepository;
        }

        public async Task<Domain.Entities.Group> GetBestGroup(Domain.Entities.Buddy buddy)
        {
            var region = await _regionRepository.GetById(buddy.RegionId);

            // Get Group Scores
            var scoreByGroup = new Dictionary<Domain.Entities.Group, double>();
            foreach (var groupId in region.GroupIds)
            {
                var group = await _groupRepository.GetById(groupId);
                var score = group.GetScore(buddy);
                if(Math.Abs(score) > 0.0)
                    scoreByGroup.Add(group, score);
            }

            // Return matched group
            if (scoreByGroup.Any())
                return scoreByGroup.OrderByDescending(x => x.Value).First().Key;

            // If there is no match, create a matching group
            var newGroupId = Guid.NewGuid().ToString();
            var newGroup = await _groupRepository.GetById(newGroupId);
            newGroup.Create(newGroupId, buddy.RegionId, buddy.GenreIds.ToList());
            return newGroup;
        }
    }
}
