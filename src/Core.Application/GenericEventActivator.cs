using System;
using Core.Application.Command;
using Core.Application.Query;
using Core.Domain;
using Core.Domain.Events;

namespace Core.Application
{
    public static class GenericEventActivator
    {
        public static object CreateCommandEvent(IEvent @event)
        {
            var generic = typeof(CommandEvent<>);
            return CreateEvent(generic, @event);
        }

        public static object CreateQueryEvent(IEvent @event)
        {
            var generic = typeof(QueryEvent<>);
            return CreateEvent(generic, @event);
        }

        private static object CreateEvent(Type generic, IEvent @event)
        {
            var type = @event.GetType();
            var genericType = generic.MakeGenericType(type);
            return Activator.CreateInstance(genericType, @event);
        }
    }
}
