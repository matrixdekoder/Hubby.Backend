using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Region.Application.CommandService.Create
{
    public class CreateRegionCommandHandler: INotificationHandler<CreateRegionCommand>
    {
        private readonly IRepository<Domain.Region> _repository;

        public CreateRegionCommandHandler(IRepository<Domain.Region> repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateRegionCommand notification, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid().ToString();
            var region = await _repository.GetById(id);
            region.Create(id, notification.Name);
            await _repository.Save(region);
        }
    }
}
