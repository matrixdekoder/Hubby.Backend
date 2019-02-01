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

        public async Task Listen()
        {
            var position = await _collection.Find(x => x.Name == EventStoreConstants.PositionKey).FirstOrDefaultAsync();
            _eventStoreContext.Connection.SubscribeToAllFrom(position.Position ?? Position.Start, CatchUpSubscriptionSettings.Default, HandleEvent);
        }

        private async Task HandleEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            var @event = resolvedEvent.DeserializeEvent();
            await _mediator.Publish(@event);
            await _collection.ReplaceOneAsync(x => x.Name == EventStoreConstants.PositionKey, new EventStorePosition(resolvedEvent.OriginalPosition));
        }
    }
}