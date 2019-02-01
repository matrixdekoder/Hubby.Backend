using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Application.QueryService
{
    public static class AccountQueryServiceBootstrap
    {
        public static void ConfigureAccountQueryServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(AccountQueryServiceBootstrap));
        }
    }
}
