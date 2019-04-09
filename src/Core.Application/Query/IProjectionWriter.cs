using System;
using System.Threading.Tasks;

namespace Core.Application.Query
{
    public interface IProjectionWriter
    {
        Task Add<T>(T view) where T : IReadModel;
        Task Update<T>(string id, Action<T> updateActions) where T : IReadModel;
    }
}
