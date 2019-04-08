using System;
using System.Threading.Tasks;
using Core.Application;
using Core.Domain;
using EventStore.ClientAPI;
using Library.EventStore.Configurations;
using Library.EventStore.Models;
using Library.Mongo.Persistence;
using MediatR;
using MongoDB.Driver;

namespace Library.EventStore.Handlers
{
    public class QueryStreamHandler : IQueryStreamHandler
    {
        private readonly IMediator _mediator;
        private readonly IMongoCollection<EventStorePosition> _collection;

        public QueryStreamHandler(IMediator mediator, IMongoContext mongoContext)
        {
            _mediator = mediator;
            _collection = mongoContext.GetCollection<EventStorePosition>();
        }

        public string Type => EventStoreConstants.QueryType;

        public async Task HandleEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent)
        {
            if (resolvedEvent.OriginalPosition == null) throw new ArgumentNullException(nameof(resolvedEvent.OriginalPosition));

            var @event = resolvedEvent.DeserializeEvent();

            if (@event != null)
            {
                var queryEvent = CreateQueryEvent(@event);
                await _mediator.Publish(queryEvent);
                await UpdatePosition(resolvedEvent.OriginalPosition.Value);
            }
        }

        private static object CreateQueryEvent(IEvent @event)
        {
            var generic = typeof(QueryEvent<>);
            var type = @event.GetType();
            var genericType = generic.MakeGenericType(type);
            return Activator.CreateInstance(genericType, @event);
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