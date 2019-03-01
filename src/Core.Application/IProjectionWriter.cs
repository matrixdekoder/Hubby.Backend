using System;
using System.Threading.Tasks;

namespace Core.Application
{
    public interface IProjectionWriter<T> where T : IReadModel
    {
        Task Add(T view);
        Task Update(string id, Action<T> updateActions);
    }
}
