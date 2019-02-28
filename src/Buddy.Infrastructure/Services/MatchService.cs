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

        public async Task<string> GetBestGroupId(Domain.Entities.Buddy buddy)
        {
            var region = await _regionRepository.GetById(buddy.RegionId);
            var groupIds = region.GroupIds.Except(new List<string> {buddy.CurrentGroupId});

            var scoreByGroup = new Dictionary<Domain.Entities.Group, double>();
            foreach (var groupId in groupIds)
            {
                var group = await _groupRepository.GetById(groupId);
                var score = group.Match(buddy);
                if(Math.Abs(score) > 0.0)
                    scoreByGroup.Add(group, score);
            }

            return !scoreByGroup.Any() ? null : scoreByGroup.OrderByDescending(x => x.Value).First().Key.Id;
        }
    }
}
