using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Buddy.Domain;
using Buddy.Domain.Services;
using Core.Domain;
using Core.Domain.Entities;
using MediatR;
using Region.Application.QueryService.GetRegion;

namespace Buddy.Infrastructure.Services
{
    public class MatchService: IMatchService
    {
        private readonly IRepository _repository;
        private readonly IMediator _mediator;

        public MatchService(IRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Group> GetBestGroup(Domain.Buddy buddy)
        {
            var region = await _mediator.Send(new GetRegionQuery(buddy.RegionId));

            // Get Group Scores
            var scoreByGroup = new Dictionary<Group, double>();
            foreach (var groupId in region.GroupIds)
            {
                var group = await _repository.GetById<Group>(groupId);
                var score = group.GetScore(buddy);
                if(Math.Abs(score) > 0.0)
                    scoreByGroup.Add(group, score);
            }

            // Return matched group
            if (scoreByGroup.Any())
                return scoreByGroup.OrderByDescending(x => x.Value).First().Key;

            // If there is no match, create a matching group
            var newGroupId = Guid.NewGuid().ToString();
            var newGroup = await _repository.GetById<Group>(newGroupId);
            newGroup.Create(newGroupId, buddy.RegionId, buddy.GenreIds.ToList());
            return newGroup;
        }
    }
}
