using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Infrastructure;
using Core.Api;
using Core.Infrastructure;
using MediatR;
using Region.Application.CommandService.Create;
using Region.Domain;

namespace Region.Api
{
    public class RegionSeeder : ISeeder
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public RegionSeeder(IMediator mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task Seed()
        {
            var commands = new List<CreateRegionCommand>
            {
                new CreateRegionCommand(RegionConstants.Europe, "Europe"),
                new CreateRegionCommand(RegionConstants.Asia, "Asia"),
                new CreateRegionCommand(RegionConstants.Australia, "Australia"),
                new CreateRegionCommand(RegionConstants.NorthAmerica, "North America"),
                new CreateRegionCommand(RegionConstants.SouthAmerica, "South America")
            };

            try
            {
                foreach (var command in commands)
                {
                    await _mediator.Publish(command);
                }

                await _unitOfWork.Commit();
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }

        }
    }
}
