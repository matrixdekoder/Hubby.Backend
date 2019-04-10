using Core.Api;
using Core.Api.Exceptions;
using Genre.Application.QueryService;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Infrastructure;
using Core.Infrastructure;
using Genre.Application.QueryService.GetGenres;

namespace Genre.Api
{
    [Route("api/[controller]")]
    public class GenreController: BaseController
    {
        public GenreController(IMediator mediator, IExceptionHandler exceptionHandler, IUnitOfWork unitOfWork) : base(mediator, exceptionHandler, unitOfWork)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            return await SendRequest<GetGenresQuery, IList<GenreReadModel>>(new GetGenresQuery());
        }
    }
}
