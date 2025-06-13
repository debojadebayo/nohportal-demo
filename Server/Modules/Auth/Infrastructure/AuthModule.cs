using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Auth.Infrastructure.Database;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Modules;
using Server.Modules.Auth.Infrastructure.Database.Extensions;

namespace Server.Modules.Auth.Infrastructure
{
	public class AuthModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<IDbContext<AuthDbContext>, AuthDbContext>(options =>
							options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

			return services;
		}
		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			if (isDevelopment)
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
					dbContext.Database.Migrate();
					RoleSeeder.SeedRolesAndPermissions(dbContext);
				}
			}
			return app;
		}
	}
}