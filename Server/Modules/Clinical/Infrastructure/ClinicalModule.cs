using ComposedHealthBase.Server.BaseModule;
using ComposedHealthBase.Server.BaseModule.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Clinical.Infrastructure.Database;

namespace Server.Modules.Clinical.Infrastructure
{
	public class ClinicalModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<ClinicalDbContext>(options =>
							options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
			return services;
		}
		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			if (isDevelopment)
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<ClinicalDbContext>();
					dbContext.Database.Migrate();
				}
			}
			return app;
		}
	}
}