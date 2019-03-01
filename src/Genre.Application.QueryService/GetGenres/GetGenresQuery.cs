using System.Collections.Generic;
using MediatR;

namespace Genre.Application.QueryService.GetGenres
{
    public class GetGenresQuery: IRequest<IList<GenreReadModel>>
    {
    }
}
