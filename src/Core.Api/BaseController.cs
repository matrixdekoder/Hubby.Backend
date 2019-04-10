using System;
using System.Threading.Tasks;
using Buddy.Infrastructure;
using Core.Api.Exceptions;
using Core.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Api
{
    [ApiController]
    [Authorize]
    public abstract class BaseController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly IUnitOfWork _unitOfWork;

        public BaseController(IMediator mediator, IExceptionHandler exceptionHandler, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _exceptionHandler = exceptionHandler;
            _unitOfWork = unitOfWork;
        }

        protected async Task<IActionResult> SendRequest<TRequest, TResponse>(TRequest request) where TRequest: IRequest<TResponse>
        {
            try
            {
                var result = await _mediator.Send(request);
                await _unitOfWork.Commit();
                return Ok(result);
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                return _exceptionHandler.Handle(e);
            }
        }

        protected async Task<IActionResult> Publish<T>(T notification) where T : INotification
        {
            try
            {
                await _mediator.Publish(notification);
                await _unitOfWork.Commit();
                return Ok();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                return _exceptionHandler.Handle(e);
            }
        }
    }
}