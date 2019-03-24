﻿using System.Threading.Tasks;
using Buddy.Application.CommandService.Buddy.ChooseGenres;
using Buddy.Application.CommandService.Buddy.ChooseRegion;
using Buddy.Application.CommandService.Buddy.LeaveGroup;
using Buddy.Application.CommandService.Buddy.MatchGroup;
using Buddy.Application.CommandService.Group.RemoveBuddy;
using Buddy.Application.QueryService.Buddy;
using Buddy.Application.QueryService.Buddy.Get;
using Buddy.Application.QueryService.Group;
using Buddy.Application.QueryService.Group.Get;
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
        public BuddyController(IMediator mediator, IExceptionHandler exceptionHandler) : base(mediator, exceptionHandler)
        {
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBuddy(string id)
        {
            return await SendRequest<GetBuddyQuery, BuddyReadModel>(new GetBuddyQuery(id));
        }

        [HttpGet("group/{id}")]
        public async Task<IActionResult> GetGroup(string id)
        {
            return await SendRequest<GetGroupQuery, GroupReadModel>(new GetGroupQuery(id));
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
