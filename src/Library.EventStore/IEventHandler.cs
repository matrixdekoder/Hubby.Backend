using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Library.EventStore
{
    public interface IEventHandler
    {
        Task HandleEvent(EventStoreCatchUpSubscription subscription, ResolvedEvent resolvedEvent);
    }
}
