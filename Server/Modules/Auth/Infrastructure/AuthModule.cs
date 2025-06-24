using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Modules.Auth.Infrastructure.Database;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Modules;
using Server.Modules.Auth.Infrastructure.Database.Extensions;
using ComposedHealthBase.Server.Mappers;
using Server.Modules.Auth.Entities;
using Shared.DTOs.Auth;
using Server.Modules.Auth.Infrastructure.Mappers;
using Server.Modules.Auth.Infrastructure.Commands;
using Server.Modules.Auth.Infrastructure.Queries;

namespace Server.Modules.Auth.Infrastructure
{
	public class AuthModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			services.AddDbContext<IDbContext<AuthDbContext>, AuthDbContext>(options =>
							options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

			// Register mappers
			services.AddScoped<IMapper<LocalStorageKey, LocalStorageKeyDto>, LocalStorageKeyMapper>();

			// Register commands and queries
			services.AddScoped<IGenerateRSAKeyCommand, GenerateRSAKeyCommand>();
			services.AddScoped<IGetLocalStorageKeyQuery, GetLocalStorageKeyQuery>();

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