using System;
using System.Threading.Tasks;
using Core.Application;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;
using Library.EventStore.Models;
using Library.Mongo.Persistence;
using MediatR;
using MongoDB.Driver;

namespace Library.EventStore.Handlers
{
    public class StreamHandler : IStreamHandler
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<EventStorePosition> _collection;

        public StreamHandler(IMediator mediator, IMongoContext mongoContext)
        {
            _mediator = mediator;
            _collection = mongoContext.GetCollection<EventStorePosition>();
        }

        public async Task HandleReadEvents(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.OriginalPosition == null) throw new ArgumentNullException(nameof(resolvedEvent.OriginalPosition));

            var @event = resolvedEvent.DeserializeEvent();

            if (@event != null)
            {
                var queryEvent = GenericEventActivator.CreateQueryEvent(@event);
                await _mediator.Publish(queryEvent);
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