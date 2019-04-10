using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain.Events;

namespace Core.Infrastructure
{
    public interface IUnitOfWork
    {
        IList<IEvent> GetEvents(string id);
        Task SaveChanges(string stream, IList<IEvent> events);
        Task Commit();
        void Rollback();
    }
}
