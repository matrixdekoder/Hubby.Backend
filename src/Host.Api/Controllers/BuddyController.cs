using System.Linq;
using System.Threading.Tasks;
using Buddy.Application.CommandService.Buddy.ChooseGenres;
using Buddy.Application.CommandService.Buddy.ChooseRegion;
using Buddy.Application.CommandService.Buddy.LeaveGroup;
using Buddy.Application.CommandService.Buddy.MatchGroup;
using Buddy.Application.QueryService.Buddy;
using Buddy.Application.QueryService.Group;
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
        private readonly IMediator _mediator;

        public BuddyController(IMediator mediator, IExceptionHandler exceptionHandler) : base(mediator, exceptionHandler)
        {
            _mediator = mediator;
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

        [HttpPost("{buddyId}/match")]
        public async Task<IActionResult> Match(string buddyId)
        {
            return await Publish(new MatchGroupCommand(buddyId));
        }

        [HttpPost("{buddyId}/leave")]
        public async Task<IActionResult> LeaveGroup(string buddyId)
        {
            return await Publish(new LeaveGroupCommand(buddyId));
        }
    }
}
