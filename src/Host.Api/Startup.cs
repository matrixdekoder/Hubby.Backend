using System;
using System.Text;
using Account.Application.CommandService;
using Account.Application.QueryService;
using Buddy.Application.CommandService;
using Buddy.Application.QueryService;
using Buddy.Infrastructure;
using Buddy.Infrastructure.Seeder;
using Core.Infrastructure;
using Host.Api.Exceptions;
using Library.EventStore;
using Library.Mongo;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Host.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Framework services
            services.AddCors(options =>
                options.AddPolicy("AllowAll", builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddMvc();

            // Library services
            services.AddMediatR(typeof(Startup));
            services.ConfigureEventStore(Configuration);
            services.ConfigureMongoDb(Configuration);

            // Core
            services.ConfigureCoreInfrastructure(Configuration);

            // Security
            UseSecurity(services);

            // Application services
            services.AddTransient<IExceptionHandler, ExceptionHandler>();
            services.ConfigureAccountCommandServices();
            services.ConfigureAccountQueryServices();
            services.ConfigureBuddyCommandServices();
            services.ConfigureBuddyQueryServices();
            services.ConfigureBuddyInfrastructure();
            services.ConfigureBuddyInfrastructureSeeder();
        }

        public void Configure(IApplicationBuilder app)
        {
            SeedDatabase(app);
            CatchUpEvents(app);
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseMvc();
        }

        private void SeedDatabase(IApplicationBuilder app)
        {
            var seed = Convert.ToBoolean(Configuration.GetSection("Seed").Value);
            if (!seed) return;

            var scope = app.ApplicationServices.CreateScope();
            var seeders = scope.ServiceProvider.GetServices<ISeeder>();
            foreach (var seeder in seeders)
            {
                seeder.Seed();
            }
        }

        private static void CatchUpEvents(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var eventStoreListener = scope.ServiceProvider.GetService<IEventStoreListener>();
            eventStoreListener.Listen();
        }

        private void UseSecurity(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Token:Secret").Value);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false, 
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}