using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
using ComposedHealthBase.Server.Queries.ModuleQueries;

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
            services.AddScoped<IMapper<Role, RoleDto>, RoleMapper>();
            services.AddScoped<IMapper<Permission, PermissionDto>, PermissionMapper>();

            // Register commands and queries
            services.AddScoped<IGenerateRSAKeyCommand, GenerateRSAKeyCommand>();
            services.AddScoped<ICreateRoleCommand, CreateRoleCommand>();
            services.AddScoped<IUpdateRoleCommand, UpdateRoleCommand>();
            services.AddScoped<IDeleteRoleCommand, DeleteRoleCommand>();
            services.AddScoped<IRevokeLocalStorageKeyCommand, RevokeLocalStorageKeyCommand>();
            services.AddScoped<IGetLocalStorageKeyQuery, GetLocalStorageKeyQuery>();
            services.AddScoped<IGetAllRolesQuery, GetAllRolesQuery>();
            services.AddScoped<IGetRoleByIdQuery, GetRoleByIdQuery>();
            services.AddScoped<IGetAllPermissionsQuery, GetAllPermissionsQuery>();
            services.AddScoped<IGetAllRolesWithPermissionsQuery, GetAllRolesQuery>();

            return services;
        }
        public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
        {
            var logger = app.Services.GetRequiredService<ILogger<AuthModule>>();
            logger.LogInformation("Configuring Auth module services...");
            
            if (isDevelopment)
            {
                using (var scope = app.Services.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
                    logger.LogInformation("Running Auth module database migrations...");
                    dbContext.Database.Migrate();
                    
                    logger.LogInformation("Seeding roles and permissions...");
                    RoleSeeder.SeedRolesAndPermissions(dbContext);
                    logger.LogInformation("Auth module database setup completed");
                }
            }
            
            logger.LogInformation("Auth module configuration completed");
            return app;
        }
    }
}