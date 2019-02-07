using System;
using Core.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Host.Api.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        public IActionResult Handle(Exception e)
        {
            switch (e)
            {
                case InvalidOperationException _:
                    return new BadRequestObjectResult(e.Message);
                case ItemNotFoundException _:
                    return new NotFoundObjectResult(e.Message);
                case UnauthorizedAccessException _:
                case SecurityTokenException _:
                    return new UnauthorizedObjectResult(e.Message);
                default:
                    return new ObjectResult(e.Message);
            }
        }
    }
}