using System.Threading.Tasks;

namespace Core.Domain
{
    public interface ISagaRepository: IRepository
    {
        Task<long> StartTransaction<T>(string aggregateId) where T : IAggregate;
        Task Save<T>(long id, T aggregate) where T : IAggregate;
        Task Commit(long id);
        void Rollback(long id);
    }
}
