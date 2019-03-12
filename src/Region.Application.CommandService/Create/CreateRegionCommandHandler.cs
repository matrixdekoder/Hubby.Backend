using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Region.Application.CommandService.Create
{
    public class CreateRegionCommandHandler: INotificationHandler<CreateRegionCommand>
    {
        private readonly IRepository _repository;

        public CreateRegionCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateRegionCommand notification, CancellationToken cancellationToken)
        {
            var region = await _repository.GetById<Domain.Region>(notification.Code);
            region.Create(notification.Code, notification.Name);
            await _repository.Save(region);
        }
    }
}
