using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Core.Application.Saga
{
    public interface ISagaOrchestrator
    {
        IDictionary<string, IList<IEvent>> TransactionEvents { get; }
        //Task<long> StartTransaction<T>(string id) where T : IAggregate;
        Task PublishCommand(INotification notification, CancellationToken token);
        void AddEventToTransaction(IEvent @event);
        IList<IEvent> GetTransactionEvents(string id);
        Task Commit();
    }
}
