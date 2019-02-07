using System.Threading.Tasks;
using Account.Application.CommandService.Register;
using Account.Application.QueryService.Login;
using Account.Application.QueryService.Refresh;
using Account.Application.QueryService.Token;
using Host.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController: BaseController
    {
        public AccountController(IMediator mediator, IExceptionHandler exceptionHandler): base(mediator, exceptionHandler)
        {
        }

        [HttpGet]
        [Authorize]
        public IActionResult Test()
        {
            return Ok("YAY");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterAccount request)
        {
            return await SendRequest<RegisterAccount, RegisterAccountResponse>(request);
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] LoginQueryModel request)
        {
            return await SendRequest<LoginQueryModel, TokenResponseModel>(request);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenQueryModel request)
        {
            return await SendRequest<RefreshTokenQueryModel, TokenResponseModel>(request);
        }
    }
}
