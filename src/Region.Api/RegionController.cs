using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Infrastructure;
using Core.Api;
using Core.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Region.Application.QueryService;
using Region.Application.QueryService.GetRegions;

namespace Region.Api
{
    [Route("api/[controller]")]
    public class RegionController: BaseController
    {
        public RegionController(IMediator mediator, IExceptionHandler exceptionHandler, IUnitOfWork unitOfWork) : base(mediator, exceptionHandler, unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            return await SendRequest<RegionsQuery, IList<RegionReadModel>>(new RegionsQuery());
        }
    }
}
