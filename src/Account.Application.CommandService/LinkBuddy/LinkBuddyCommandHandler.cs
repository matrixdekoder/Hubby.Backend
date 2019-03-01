using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Account.Application.CommandService.LinkBuddy
{
    public class LinkBuddyCommandHandler: INotificationHandler<LinkBuddyCommand>
    {
        private readonly IRepository<Domain.Account> _repository;

        public LinkBuddyCommandHandler(IRepository<Domain.Account> repository)
        {
            _repository = repository;
        }

        public async Task Handle(LinkBuddyCommand notification, CancellationToken cancellationToken)
        {
            var account = await _repository.GetById(notification.AccountId);
            account.LinkBuddy(notification.BuddyId);
            await _repository.Save(account);
        }
    }
}
