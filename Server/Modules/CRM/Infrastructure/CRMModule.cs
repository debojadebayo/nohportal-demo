using ComposedHealthBase.Server;
using ComposedHealthBase.Server.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.CRM.Infrastructure.Database;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Modules;
using Server.Modules.CRM.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;

namespace Server.Modules.CRM.Infrastructure
{
	public class CRMModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<IDbContext<CRMDbContext>, CRMDbContext>(options =>
							options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

			// services.Configure<AuthorizationOptions>(options =>
			// {
			// 	CRMPolicyBuilder.AddCRMPolicies(options);
			// });

			// services.AddScoped<IEntityPermissionService, CRMRolePermissionService>();

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