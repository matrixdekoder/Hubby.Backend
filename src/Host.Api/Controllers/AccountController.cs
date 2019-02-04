using System;
using System.Threading.Tasks;
using Account.Application.CommandService.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterAccount registerAccount)
        {
            try
            {
                var response = await _mediator.Send(registerAccount);
                return Ok(response);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
