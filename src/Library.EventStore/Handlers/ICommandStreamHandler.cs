using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Library.EventStore.Handlers
{
    public interface ICommandStreamHandler
    {
        Task HandleEvent(EventStoreSubscription subscription, ResolvedEvent resolvedEvent);
    }
}
