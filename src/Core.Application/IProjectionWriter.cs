using System;
using System.Threading.Tasks;

namespace Core.Application
{
    public interface IProjectionWriter
    {
        Task Add<T>(T view) where T : IReadModel;
        Task Update<T>(string id, Action<T> updateActions) where T : IReadModel;
    }
}
