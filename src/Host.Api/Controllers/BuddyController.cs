using System.Threading.Tasks;
using Buddy.Application.CommandService.ChooseGenres;
using Buddy.Application.CommandService.ChooseRegion;
using Buddy.Application.QueryService.Status;
using Buddy.Domain.Enums;
using Host.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [Route("api/[controller]")]
    public class BuddyController: BaseController
    {
        public BuddyController(IMediator mediator, IExceptionHandler exceptionHandler) : base(mediator, exceptionHandler)
        {
        }

        [HttpPost("region")]
        public async Task<IActionResult> ChooseRegion([FromBody] ChooseRegionCommand command)
        {
            return await Publish(command);
        }

        [HttpPost("genre")]
        public async Task<IActionResult> ChooseGenres([FromBody] ChooseGenresCommand command)
        {
            return await Publish(command);
        }

        [HttpGet("{id}/status")]
        public async Task<IActionResult> GetStatus(string id)
        {
            return await SendRequest<BuddyStatusQuery, BuddyStatus>(new BuddyStatusQuery(id));
        }
    }
}
