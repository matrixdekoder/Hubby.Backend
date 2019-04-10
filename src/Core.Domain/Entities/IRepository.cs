using System.Threading.Tasks;

namespace Core.Domain.Entities
{
    public interface IRepository 
    {
        Task<T> GetById<T>(string id) where T : IAggregate, new();
        Task Save<T>(T aggregate) where T : IAggregate;
    }
}
