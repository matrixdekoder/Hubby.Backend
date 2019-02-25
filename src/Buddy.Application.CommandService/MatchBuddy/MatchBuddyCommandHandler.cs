using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Entities;
using Core.Application.Exceptions;
using Library.EventStore;
using MediatR;

namespace Buddy.Application.CommandService.MatchBuddy
{
    public class MatchBuddyCommandHandler: INotificationHandler<MatchBuddyCommand>
    {
        private readonly IEventStoreRepository<Domain.Entities.Buddy> _buddyRepository;
        private readonly IEventStoreRepository<Group> _groupRepository;

        public MatchBuddyCommandHandler(
            IEventStoreRepository<Domain.Entities.Buddy> buddyRepository, 
            IEventStoreRepository<Domain.Entities.Group> groupRepository)
        {
            _buddyRepository = buddyRepository;
            _groupRepository = groupRepository;
        }

        public async Task Handle(MatchBuddyCommand notification, CancellationToken cancellationToken)
        {
            // Retrieve buddy
            var buddy = await _buddyRepository.GetById(notification.BuddyId);

            // Retrieve groups
            var groups = new List<Group>();
            foreach (var groupId in notification.GroupIds)
            {
                groups.Add(await _groupRepository.GetById(groupId));
            }

            // Calculate score by group
            var scoreByGroup = new Dictionary<string, double>();
            foreach (var group in groups)
            {
                var score = group.Match(buddy);
                scoreByGroup.Add(group.Id, score);
            }

            // TODO: Check for matches
            if (scoreByGroup.Values.All(score => Math.Abs(score) <= 0.0))
            {
                // TODO: If no matches found, create new group
            }
            else
            {
                // TODO: If there is / are matches, add to group
            }

        }
    }
}
