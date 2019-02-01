using System.Threading.Tasks;

namespace Library.EventStore
{
    public interface IEventStoreListener
    {
        void Listen();
    }
}
