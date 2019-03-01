using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Genre.Application.QueryService
{
    public static class GenreQueryServiceBootstrap
    {
        public static void ConfigureGenreQueryService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GenreQueryServiceBootstrap));
        }
    }
}
