using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Genre.Application.CommandService
{
    public static class GenreCommandServiceBootstrap
    {
        public static void ConfigureGenreCommandService(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GenreCommandServiceBootstrap));
        }
    }
}
