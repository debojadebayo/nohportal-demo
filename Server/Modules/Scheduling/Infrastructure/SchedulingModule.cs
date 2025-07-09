using ComposedHealthBase.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Scheduling.Infrastructure.Database;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Modules;
using Server.Modules.Scheduling.Infrastructure.Queries;
using Server.Modules.Shared.Contracts;
using Microsoft.AspNetCore.Authorization;
using Server.Modules.Scheduling.Infrastructure.AuthorizationHandlers;
using ComposedHealthBase.Server.Auth.Providers;

namespace Server.Modules.Scheduling.Infrastructure
{
    public class SchedulingModule : IModule
    {
        public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<IDbContext<SchedulingDbContext>, SchedulingDbContext>(options =>
                            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IGetSchedulesForInvoiceQuery, GetSchedulesForInvoiceQuery>();

            services.AddScoped<IAuthorizationHandler, SchedulingResourceAccessAuthorizationHandler>();

            ResourceAccessPolicyProvider.AddRequirement(new SchedulingResourceAccessRequirement());

            return services;
        }
        public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
        {
            if (isDevelopment)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<SchedulingDbContext>();
                    dbContext.Database.Migrate();
                }
            }
            return app;
        }
    }
}