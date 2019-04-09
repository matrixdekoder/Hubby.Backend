using System;
using System.Threading;
using System.Threading.Tasks;
using Buddy.Application.CommandService.Buddy.JoinGroup;
using Buddy.Application.CommandService.Buddy.LeaveGroup;
using Buddy.Application.CommandService.Group.Clear;
using Buddy.Application.CommandService.Group.Match;
using Buddy.Application.CommandService.Group.MergeBuddies;
using Buddy.Application.CommandService.Group.RemoveBuddy;
using Buddy.Application.CommandService.Group.SetStatus;
using Buddy.Application.CommandService.Group.StartMerge;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Application.Saga;
using MediatR;

namespace Buddy.Application.CommandService.Group.RemoveBuddySaga
{
    public class RemoveBuddySaga:
        INotificationHandler<RemoveBuddySagaCommand>,
        ISagaEventListener<BuddyRemoved>,
        ISagaEventListener<GroupMatched>,
        ISagaEventListener<MergeStarted>,
        ISagaEventListener<BuddyAdded>,
        ISagaEventListener<BuddiesMerged>,
        ISagaEventListener<GroupIsCleared>
    {
        private readonly ISagaOrchestrator _orchestrator;

        public RemoveBuddySaga(ISagaOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public async Task Handle(RemoveBuddySagaCommand notification, CancellationToken cancellationToken)
        {
            var command = new RemoveBuddyCommand(notification.BuddyId, notification.GroupId);
            await _orchestrator.PublishCommand(command, cancellationToken);
        }

        public async Task Handle(SagaEvent<BuddyRemoved> notification, CancellationToken cancellationToken)
        {
            _orchestrator.AddEventToTransaction(notification.Event);

            var leaveGroupCommand = new LeaveGroupCommand(notification.Event.BuddyId);
            await _orchestrator.PublishCommand(leaveGroupCommand, cancellationToken);

            var matchGroupCommand = new MatchGroupCommand(notification.Event.Id, notification.TransactionId);
            await _orchestrator.PublishCommand(matchGroupCommand, cancellationToken);
        }

        public async Task Handle(SagaEvent<GroupMatched> notification, CancellationToken cancellationToken)
        {
            _orchestrator.AddEventToTransaction(notification.Event);

            var startMergeCommand = new StartGroupMergeCommand(notification.Event.Id, notification.Event.MatchId, notification.TransactionId);
            await _orchestrator.PublishCommand(startMergeCommand, cancellationToken);
        }

        public async Task Handle(SagaEvent<MergeStarted> notification, CancellationToken cancellationToken)
        {
            _orchestrator.AddEventToTransaction(notification.Event);

            var setStatusCommand = new SetGroupStatusCommand(notification.Event.MatchedGroupId, GroupStatus.Merging);
            await _orchestrator.PublishCommand(setStatusCommand, cancellationToken);

            var events = _orchestrator.GetTransactionEvents(notification.Event.Id);
            var mergeBuddiesCommand = new MergeBuddiesCommand(notification.Event.Id, notification.Event.MatchedGroupId, notification.TransactionId, events, matchedGroupTransaction);
            await _orchestrator.PublishCommand(mergeBuddiesCommand, cancellationToken);
        }

        public async Task Handle(SagaEvent<BuddyAdded> notification, CancellationToken cancellationToken)
        {
            _orchestrator.AddEventToTransaction(notification.Event);

            var buddyTransactionId = await _orchestrator.StartTransaction<Domain.Buddy>(notification.Event.BuddyId);
            var joinGroupCommand = new JoinGroupCommand(notification.Event.BuddyId, notification.Event.Id, notification.Event.Type, buddyTransactionId);
            await _orchestrator.PublishCommand(joinGroupCommand, cancellationToken);
        }

        public async Task Handle(SagaEvent<BuddiesMerged> notification, CancellationToken cancellationToken)
        {
            _orchestrator.AddEventToTransaction(notification.Event);

            var clearGroupCommand = new ClearGroupCommand(notification.Event.MatchedGroupId, notification.Event.MatchedGroupTransaction);
            await _orchestrator.PublishCommand(clearGroupCommand, cancellationToken);
        }

        public async Task Handle(SagaEvent<GroupIsCleared> notification, CancellationToken cancellationToken)
        {
            await _orchestrator.Commit();
        }
    }
}
