using EventStore.ClientAPI;

namespace Library.EventStore.Persistence
{
    public interface IEventStoreContext
    {
        IEventStoreConnection Connection { get; }
    }
}
