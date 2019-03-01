using System.Collections.Generic;
using MediatR;

namespace Buddy.Application.QueryService.Genre
{
    public class GenresQuery: IRequest<IList<GenreReadModel>>
    {
    }
}
