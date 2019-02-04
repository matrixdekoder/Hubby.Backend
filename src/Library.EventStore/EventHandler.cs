using System;
using System.Threading.Tasks;
using EventStore.ClientAPI;
using Library.Mongo;
using MediatR;
using MongoDB.Driver;

namespace Library.EventStore
{
    public class EventHandler : IEventHandler
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<EventStorePosition> _collection;

        public EventHandler(IMediator mediator, IMongoContext mongoContext)
        {
            _mediator = mediator;
            _collection = mongoContext.GetCollection<EventStorePosition>();
        }

        public async Task HandleEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.OriginalPosition == null) throw new ArgumentNullException(nameof(resolvedEvent.OriginalPosition));

            var @event = resolvedEvent.DeserializeEvent();
            if (@event != null)
            {
                await _mediator.Publish(@event);
                await UpdatePosition(resolvedEvent.OriginalPosition.Value);
            }
        }

        private async Task UpdatePosition(Position newPosition)
        {
            var position = await _collection.Find(x => x.Name == EventStoreConstants.PositionKey).FirstOrDefaultAsync();
            if (position == null) throw new ArgumentNullException(nameof(position));

            position.CommitPosition = newPosition.CommitPosition;
            position.PreparedPosition = newPosition.PreparePosition;

            await _collection.FindOneAndReplaceAsync(x => x.Name == position.Name, position);
        }
    }
}