using EventStore.ClientAPI;

namespace Library.EventStore
{
    public interface IEventStoreContext
    {
        IEventStoreConnection Connection { get; }
    }
}
