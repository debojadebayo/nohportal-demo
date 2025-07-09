using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.CRM.Infrastructure.Database;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Modules;
using Server.Modules.Shared.Contracts;
using ComposedHealthBase.Server.Queries;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;
using Server.Modules.CRM.Infrastructure.Queries;
using Microsoft.AspNetCore.Authorization;
using Server.Modules.CRM.Infrastructure.AuthorizationHandlers;
using ComposedHealthBase.Server.Auth.Providers;

namespace Server.Modules.CRM.Infrastructure
{
    public class CRMModule : IModule
    {
        public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<IDbContext<CRMDbContext>, CRMDbContext>(options =>
                            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IGetCustomerByIdQuery, GetCustomerByIdQuery>();
            services.AddScoped<IGetProductsByIdsQuery, GetProductsByIdsQuery>();

            services.AddScoped<IAuthorizationHandler, CRMResourceAccessAuthorizationHandler>();

            ResourceAccessPolicyProvider.AddRequirement(new CRMResourceAccessRequirement());

            return services;
        }
        public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
        {
            if (isDevelopment)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<CRMDbContext>();
                    dbContext.Database.Migrate();
                }
            }
            return app;
        }
    }
}