using System;
using System.Threading.Tasks;
using Host.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [ApiController]
    public abstract class BaseController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IExceptionHandler _exceptionHandler;

        public BaseController(IMediator mediator, IExceptionHandler exceptionHandler)
        {
            _mediator = mediator;
            _exceptionHandler = exceptionHandler;
        }

        protected async Task<IActionResult> SendRequest<TRequest, TResponse>(TRequest request) where TRequest: IRequest<TResponse>
        {
            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (Exception e)
            {
                return _exceptionHandler.Handle(e);
            }
        }

        protected async Task<IActionResult> Publish<T>(T notification) where T : INotification
        {
            try
            {
                await _mediator.Publish(notification);
                return Ok();
            }
            catch (Exception e)
            {
                return _exceptionHandler.Handle(e);
            }
        }
    }
}