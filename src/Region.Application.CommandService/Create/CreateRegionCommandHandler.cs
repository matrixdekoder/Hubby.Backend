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
            var region = await _repository.GetById(notification.Code);
            region.Create(notification.Code, notification.Name);
            await _repository.Save(region);
        }
    }
}
