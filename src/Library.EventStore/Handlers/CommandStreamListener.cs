using Library.EventStore.Models;
using Library.EventStore.Persistence;

namespace Library.EventStore.Handlers
{
    public class CommandStreamListener: IEventStoreListener
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly ICommandStreamHandler _eventHandler;

        public CommandStreamListener(IEventStoreContext eventStoreContext,  ICommandStreamHandler eventHandler)
        {
            _eventStoreContext = eventStoreContext;
            _eventHandler = eventHandler;
        }

        public string Type => EventStoreConstants.CommandType;

        public void Listen()
        {
            _eventStoreContext.Connection.SubscribeToAllAsync(false, _eventHandler.HandleEvent).Wait();
        }
    }
}
