using ComposedHealthBase.Server;
using ComposedHealthBase.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Scheduling.Infrastructure.Database;
using ComposedHealthBase.Server.Database;

namespace Server.Modules.Scheduling.Infrastructure
{
	public class ScheduleModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<IDbContext, ScheduleDbContext>(options =>
							options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

			return services;
		}
		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			if (isDevelopment)
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<ScheduleDbContext>();
					dbContext.Database.Migrate();
				}
			}
			return app;
		}
	}
}