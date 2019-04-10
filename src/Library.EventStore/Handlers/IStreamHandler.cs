using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Library.EventStore.Handlers
{
    public interface IStreamHandler
    {
        Task HandleReadEvents(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent);
    }
}
