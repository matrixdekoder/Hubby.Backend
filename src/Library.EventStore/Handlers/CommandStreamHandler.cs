using System;
using System.Threading.Tasks;
using Core.Application;
using Core.Domain;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;
using Library.EventStore.Models;
using MediatR;

namespace Library.EventStore.Handlers
{
    public class CommandStreamHandler: ICommandStreamHandler
    {
        private readonly IMediator _mediator;

        public CommandStreamHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public string Type => EventStoreConstants.CommandType;

        public async Task HandleEvent(EventStoreSubscription subscription, ResolvedEvent resolvedEvent)
        {
            var @event = resolvedEvent.DeserializeEvent();
            if (@event != null)
            {
                var commandEvent = CreateCommandEvent(@event);
                await _mediator.Publish(commandEvent);
            }
        }

        private static object CreateCommandEvent(IEvent @event)
        {
            var generic = typeof(CommandEvent<>);
            var type = @event.GetType();
            var genericType = generic.MakeGenericType(type);
            return Activator.CreateInstance(genericType, @event);
        }
    }
}
