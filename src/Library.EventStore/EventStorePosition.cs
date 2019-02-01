namespace Library.EventStore
{
    public class EventStorePosition
    {
        public string Stream => EventStoreConstants.Stream;

        public long? Position { get; }

        public EventStorePosition(long? position)
        {
            Position = position;
        }
    }
}
