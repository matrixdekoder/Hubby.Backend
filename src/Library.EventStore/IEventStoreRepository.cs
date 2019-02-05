using System.Threading.Tasks;
using Core.Domain;

namespace Library.EventStore
{
    public interface IEventStoreRepository<T> : IRepository<T> where T : IAggregate
    {
    }
}
