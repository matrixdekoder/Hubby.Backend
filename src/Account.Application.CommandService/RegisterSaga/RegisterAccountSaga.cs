using System;
using System.Threading;
using System.Threading.Tasks;
using Account.Application.CommandService.Register;
using Account.Domain;
using Buddy.Application.CommandService.Buddy.Create;
using Buddy.Domain.Events;
using Core.Application.Saga;
using MediatR;

namespace Account.Application.CommandService.RegisterSaga
{
    public class RegisterAccountSaga:
        INotificationHandler<RegisterAccountSagaCommand>,
        ISagaEventListener<AccountRegistered>,
        ISagaEventListener<BuddyCreated>
    {
        private readonly ISagaOrchestrator _orchestrator;

        public RegisterAccountSaga(ISagaOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        public async Task Handle(RegisterAccountSagaCommand command, CancellationToken token)
        {
            var transactionId = await _orchestrator.StartTransaction<Domain.Account>(command.Id);
            var registerAccountCommand = new RegisterAccountCommand(command.Id, command.Password, transactionId);
            await _orchestrator.PublishCommand(registerAccountCommand, token);
        }

        public async Task Handle(SagaEvent<AccountRegistered> notification, CancellationToken cancellationToken)
        {
            var transactionId = await _orchestrator.StartTransaction<Buddy.Domain.Buddy>(notification.Event.Id);
            var command = new CreateBuddyCommand(notification.Event.Id, transactionId);
            await _orchestrator.PublishCommand(command, cancellationToken);
        }

        public async Task Handle(SagaEvent<BuddyCreated> notification, CancellationToken cancellationToken)
        {
            await _orchestrator.Commit();
        }
    }
}
