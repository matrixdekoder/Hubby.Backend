using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Core.Application.Query
{
    public abstract class QueryListener<T>: INotificationHandler<QueryEvent<T>> where T : IEvent
    {
        protected readonly IProjectionWriter Writer;

        protected QueryListener(IProjectionWriter writer)
        {
            Writer = writer;
        }

        public async Task Handle(QueryEvent<T> notification, CancellationToken cancellationToken)
        {
            await Handle(notification.Event);
        }

        protected abstract Task Handle(T notification);
    }
}
