﻿using System.Threading;
using System.Threading.Tasks;
using Buddy.Domain.Events;
using MediatR;

namespace Buddy.Application.CommandService.Group.StopMerge
{
    public class StopGroupBuddiesMergedListener: INotificationHandler<BuddiesMerged>
    {
        private readonly IMediator _mediator;

        public StopGroupBuddiesMergedListener(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BuddiesMerged notification, CancellationToken cancellationToken)
        {
            var command = new StopGroupMergeCommand(notification.Id, notification.MatchedGroupId);
            await _mediator.Publish(command, cancellationToken);
        }
    }
}