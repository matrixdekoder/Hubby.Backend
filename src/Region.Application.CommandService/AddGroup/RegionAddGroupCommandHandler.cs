using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Region.Application.CommandService.AddGroup
{
    public class RegionAddGroupCommandHandler: INotificationHandler<RegionAddGroupCommand>
    {
        private readonly IRepository _repository;

        public RegionAddGroupCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(RegionAddGroupCommand notification, CancellationToken cancellationToken)
        {
            var region = await _repository.GetById<Domain.Region>(notification.RegionId);
            region.AddGroup(notification.GroupId);
            await _repository.Save(region);
        }
    }
}
