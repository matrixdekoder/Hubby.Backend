using System;
using System.Threading.Tasks;
using Core.Domain;

namespace Core.Application
{
    public interface IProjectionWriter<T> where T : IEntity
    {
        Task Add(T view);
        Task Update(Guid id, Action<T> updateActions);
    }
}
