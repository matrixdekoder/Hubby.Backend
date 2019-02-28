using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Group.Create
{
    public class CreateGroupCommandHandler: INotificationHandler<CreateGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Group> _repository;

        public CreateGroupCommandHandler(IRepository<Domain.Entities.Group> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateGroupCommand notification, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid().ToString();
            var group = await _repository.GetById(id);
            group.Create(id, notification.RegionId, notification.GenreIds);
            await _repository.Save(group);
        }
    }
}
