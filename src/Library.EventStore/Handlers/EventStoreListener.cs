using EventStore.ClientAPI;
using Library.EventStore.Models;
using Library.EventStore.Persistence;
using Library.Mongo.Persistence;
using MongoDB.Driver;

namespace Library.EventStore.Handlers
{
    public class EventStoreListener : IEventStoreListener
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly IStreamHandler _eventHandler;
        private readonly IMongoCollection<EventStorePosition> _collection;

        public EventStoreListener(IEventStoreContext eventStoreContext, IMongoContext mongoContext, IStreamHandler eventHandler)
        {
            _eventStoreContext = eventStoreContext;
            _eventHandler = eventHandler;
            _collection = mongoContext.GetCollection<EventStorePosition>();
        }

        public void Listen()
        {
            Position? position = Position.Start;

            if (_collection.AsQueryable().Any())
            {
                var result = _collection.Find(x => x.Name == EventStoreConstants.PositionKey).FirstOrDefault();
                position = new Position(result.CommitPosition, result.PreparedPosition);
            }
            else
            {
                _collection.InsertOne(new EventStorePosition
                {
                    Name = EventStoreConstants.PositionKey, CommitPosition = position.Value.CommitPosition, PreparedPosition = position.Value.PreparePosition
                });
            }

            _eventStoreContext.Connection.SubscribeToAllFrom(position, CatchUpSubscriptionSettings.Default, _eventHandler.HandleReadEvents);
        }
    }
}