using System.Threading.Tasks;

namespace Core.Domain
{
    public interface IRepository<T> where T : IAggregate
    {
        Task<T> GetById(string id);
        Task Save(T aggregate);
    }
}
