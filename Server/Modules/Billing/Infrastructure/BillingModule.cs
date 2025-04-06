using ComposedHealthBase.Server.BaseModule;
using ComposedHealthBase.Server.BaseModule.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Billing.Infrastructure.Database;

namespace Server.Modules.Billing.Infrastructure
{
	public class BillingModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<BillingDbContext>(options =>
			{
				options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Server.Modules.Billing.Infrastructure"));
			});
			return services;
		}
		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			if (isDevelopment)
			{
				using (var scope = app.Services.CreateScope())
				{
					var dbContext = scope.ServiceProvider.GetRequiredService<BillingDbContext>();
					dbContext.Database.Migrate();
				}
			}
			return app;
		}
	}
}