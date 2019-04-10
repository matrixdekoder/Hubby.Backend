using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Domain;
using Core.Domain.Events;
using EventStore.ClientAPI;

namespace Buddy.Infrastructure
{
    public interface IUnitOfWork
    {
        Task Commit();
        void Rollback();
        Task<EventStoreTransaction> GetTransaction(string id, string stream);
        IList<IEvent> GetUncommittedEvents(string id);
        void WriteEvents(string id, IList<IEvent> events);
    }
}
