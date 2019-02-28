using System.Threading;
using System.Threading.Tasks;
using Core.Domain;
using MediatR;

namespace Buddy.Application.CommandService.Region.AddGroup
{
    public class RegionAddGroupCommandHandler: INotificationHandler<RegionAddGroupCommand>
    {
        private readonly IRepository<Domain.Entities.Region> _repository;

        public RegionAddGroupCommandHandler(IRepository<Domain.Entities.Region> repository)
        {
            _repository = repository;
        }

        public async Task Handle(RegionAddGroupCommand notification, CancellationToken cancellationToken)
        {
            var region = await _repository.GetById(notification.RegionId);
            region.AddGroup(notification.GroupId);
            await _repository.Save(region);
        }
    }
}
