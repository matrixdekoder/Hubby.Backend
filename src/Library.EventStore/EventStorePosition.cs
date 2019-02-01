using EventStore.ClientAPI;

namespace Library.EventStore
{
    public class EventStorePosition
    {
        public string Name => EventStoreConstants.PositionKey;

        public Position? Position { get; }

        public EventStorePosition(Position? position)
        {
            Position = position;
        }
    }
}
