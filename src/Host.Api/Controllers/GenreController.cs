using System.Collections.Generic;
using System.Threading.Tasks;
using Buddy.Application.QueryService.Genre;
using Host.Api.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Api.Controllers
{
    [Route("api/[controller]")]
    public class GenreController: BaseController
    {
        public GenreController(IMediator mediator, IExceptionHandler exceptionHandler) : base(mediator, exceptionHandler)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            return await SendRequest<GenresQuery, IList<GenreReadModel>>(new GenresQuery());
        }
    }
}
