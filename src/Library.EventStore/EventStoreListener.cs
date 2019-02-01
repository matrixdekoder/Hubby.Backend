using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Library.EventStore
{
    public class EventStoreListener : IEventStoreListener
    {
        private readonly IEventStoreContext _eventStoreContext;
        private readonly IMediator _mediator;
        private readonly IMongoCollection<EventStorePosition> _collection;

        public EventStoreListener(IEventStoreContext eventStoreContext, IMediator mediator, IMongoContext mongoContext)
        {
            _eventStoreContext = eventStoreContext;
            _mediator = mediator;
            _collection = mongoContext.GetCollection<EventStorePosition>();
        }

        public void Listen()
        {
            Position? position = Position.Start;

            if (_collection.AsQueryable().Any())
            {
                var result = _collection.Find(x => x.Name == EventStoreConstants.PositionKey).FirstOrDefault();
                position = new Position(result.CommitPosition, result.PreparePosition);
            }
            else
            {
                _collection.InsertOne(position.ToEventStorePosition());
            }

            _eventStoreContext.Connection.SubscribeToAllFrom(position, CatchUpSubscriptionSettings.Default, HandleEvent);
        }

        private async Task HandleEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.OriginalPosition == null) throw new ArgumentNullException(nameof(resolvedEvent.OriginalPosition));

            var @event = resolvedEvent.DeserializeEvent();
            if (@event != null)
            {
                await _mediator.Publish(@event);
                await _collection.ReplaceOneAsync(x => x.Name == EventStoreConstants.PositionKey, resolvedEvent.OriginalPosition.ToEventStorePosition());
            }
        }
    }
}