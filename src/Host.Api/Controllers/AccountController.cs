using System.Threading.Tasks;
using Account.Application.CommandService.Register;
using Account.Application.QueryService.Login;
using Host.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController: BaseController
    {
        public AccountController(IMediator mediator, IExceptionHandler exceptionHandler): base(mediator, exceptionHandler)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAccount request)
        {
            return await SendRequest<RegisterAccount, RegisterAccountResponse>(request);
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] LoginQueryModel request)
        {
            return await SendRequest<LoginQueryModel, LoginTokenResponse>(request);
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Test Works");
        }
    }
}
