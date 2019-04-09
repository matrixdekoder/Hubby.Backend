using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application.CommandService
{
    public static class AccountCommandServiceBootstrap
    {
        public static void ConfigureAccountCommandServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AccountCommandServiceBootstrap));
        }
    }
}
