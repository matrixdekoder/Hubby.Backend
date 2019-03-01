using Core.Api;
using Genre.Application.CommandService;
using Genre.Application.QueryService;
using Microsoft.Extensions.DependencyInjection;

namespace Genre.Api
{
    public static class GenreBootstrap
    {
        public static void ConfigureGenre(this IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(GenreBootstrap).Assembly);
            services.ConfigureGenreQueryService();
            services.ConfigureGenreCommandService();
            services.AddTransient<ISeeder, GenreSeeder>();
        }
    }
}
