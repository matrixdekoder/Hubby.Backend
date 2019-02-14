using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Application.QueryService.Region;
using Buddy.Domain;
using Host.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RegionController: BaseController
    {
        public RegionController(IMediator mediator, IExceptionHandler exceptionHandler) : base(mediator, exceptionHandler)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            return await SendRequest<RegionsQuery, IList<Region>>(new RegionsQuery());
        }
    }
}
