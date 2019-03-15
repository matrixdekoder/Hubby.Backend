using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Library.EventStore.Handlers
{
    public interface IEventHandler
    {
        Task HandleEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent);
    }
}
