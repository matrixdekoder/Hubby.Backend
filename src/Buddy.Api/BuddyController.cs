using System.Threading.Tasks;
using Buddy.Application.CommandService.Buddy.ChooseGenres;
using Buddy.Application.CommandService.Buddy.ChooseRegion;
using Buddy.Application.CommandService.Group.MatchBuddy;
using Buddy.Application.CommandService.Group.RemoveBuddy;
using Buddy.Application.QueryService.Status;
using Buddy.Domain.Enums;
using Core.Api;
using Core.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Buddy.Api
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

        [HttpPost("match")]
        public async Task<IActionResult> Match([FromBody] MatchBuddyCommand command)
        {
            return await Publish(command);
        }

        [HttpPost("leave")]
        public async Task<IActionResult> LeaveGroup([FromBody] RemoveBuddyCommand command)
        {
            return await Publish(command);
        }
    }
}
