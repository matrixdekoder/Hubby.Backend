using System.Threading;
using System.Threading.Tasks;
using Buddy.Application.CommandService.Buddy.JoinGroup;
using Buddy.Application.CommandService.Group.MatchBuddy;
using Buddy.Domain.Enums;
using Buddy.Domain.Events;
using Core.Application.Saga;
using MediatR;
using Region.Application.CommandService.AddGroup;

namespace Buddy.Application.CommandService.Group.MatchBuddySaga
{
    public class MatchBuddySaga:
        INotificationHandler<MatchBuddySagaCommand>,
        ISagaEventListener<BuddyAdded>,
        ISagaEventListener<GroupCreated>,
        ISagaEventListener<GroupJoined>
    {
        private readonly ISagaOrchestrator _orchestrator;

        public MatchBuddySaga(ISagaOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public async Task Handle(MatchBuddySagaCommand notification, CancellationToken cancellationToken)
        {
            var command = new MatchBuddyCommand(notification.BuddyId);
            await _orchestrator.PublishCommand(command, cancellationToken);
        }

        public async Task Handle(SagaEvent<GroupCreated> notification, CancellationToken cancellationToken)
        {
            _orchestrator.AddEventToTransaction(notification.TransactionId, notification.Event);

            var regionAddGroupCommand = new RegionAddGroupCommand(notification.Event.RegionId, notification.Event.Id);
            await _orchestrator.PublishCommand(regionAddGroupCommand, cancellationToken);
        }

        public async Task Handle(SagaEvent<BuddyAdded> notification, CancellationToken cancellationToken)
        {
            _orchestrator.AddEventToTransaction(notification.TransactionId, notification.Event);

            var buddyTransactionId = await _orchestrator.StartTransaction<Domain.Buddy>(notification.Event.BuddyId);
            var joinGroupCommand = new JoinGroupCommand(notification.Event.BuddyId, notification.Event.Id,BuddyJoinType.New, buddyTransactionId);
            await _orchestrator.PublishCommand(joinGroupCommand, cancellationToken);
        }

        public async Task Handle(SagaEvent<GroupJoined> notification, CancellationToken cancellationToken)
        {
            await _orchestrator.Commit();
        }
    }
}
