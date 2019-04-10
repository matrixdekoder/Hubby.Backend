using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Events;
using MediatR;

namespace Core.Application.Command
{
    public abstract class CommandListener<T> : INotificationHandler<CommandEvent<T>> where T : IEvent
    {
        protected readonly IMediator Mediator;

        protected CommandListener(IMediator mediator)
        {
            Mediator = mediator;
        }

        public async Task Handle(CommandEvent<T> notification, CancellationToken cancellationToken)
        {
            await Handle(notification.Event);
        }

        protected abstract Task Handle(T notification);
    }
}
