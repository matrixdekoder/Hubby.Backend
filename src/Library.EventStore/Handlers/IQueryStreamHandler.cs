using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Library.EventStore.Handlers
{
    public interface IQueryStreamHandler
    {
        Task HandleEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent);
    }
}
