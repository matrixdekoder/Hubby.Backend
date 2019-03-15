using System;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;
using Microsoft.Extensions.Options;

namespace Library.EventStore.Persistence
{
    public class EventStoreContext: IEventStoreContext
    {
        public EventStoreContext(IOptions<EventStoreConfiguration> options)
        {
            var config = options.Value;
            var endpoint = new Uri($"tcp://{config.Username}:{config.Password}@{config.Address}:{config.Port}");
            Connection = EventStoreConnection.Create(ConnectionSettings.Default, endpoint);
            Connection.ConnectAsync().Wait();
        }

        public IEventStoreConnection Connection { get; }
    }
}
