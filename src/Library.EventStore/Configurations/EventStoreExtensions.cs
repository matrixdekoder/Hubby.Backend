using System;
using System.Collections.Generic;
using System.Text;
using Core.Domain;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Library.EventStore.Configurations
{
    public static class EventStoreExtensions
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };

        public static IEvent DeserializeEvent(this ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.OriginalStreamId.StartsWith("$")) return null;
            var eventTypeName = JObject.Parse(Encoding.UTF8.GetString(resolvedEvent.OriginalEvent.Metadata)).Property("Type").Value;
            var type = Type.GetType((string)eventTypeName);
            return (IEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(resolvedEvent.OriginalEvent.Data), type);
        }

        public static EventData ToEventData(this IEvent message)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message, SerializerSettings));

            var headers = new Dictionary<string, object>
            {
                {"Type", message.GetType().AssemblyQualifiedName}
            };

            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headers, SerializerSettings));
            var typeName = message.GetType().Name;
            var eventId = Guid.NewGuid();

            return new EventData(eventId, typeName, true, data, metadata);
        }
    }
}
