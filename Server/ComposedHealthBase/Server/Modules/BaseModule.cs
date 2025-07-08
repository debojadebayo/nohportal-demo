using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ComposedHealthBase.Server.Auth.AuthorizationHandlers;
using ComposedHealthBase.Server.Auth.Requirements;
using ComposedHealthBase.Server.Auth.Constants;
using ComposedHealthBase.Server.Auth.Extensions;
using ComposedHealthBase.Server.Auth.Providers;
using ComposedHealthBase.Server.Config;
using ComposedHealthBase.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Reflection;

namespace ComposedHealthBase.Server.Modules
{
	public class BaseModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<AppOptions>(configuration);
			services.AddSingleton<IKeycloakService, KeycloakService>();

			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddCors(options =>
			{
				options.AddPolicy("Client",
					policy => policy
						.WithOrigins(configuration["Cors:AllowedClientOrigin"] ?? throw new InvalidOperationException("AllowedClientOrigin not configured."),
							configuration["Cors:AllowedServerOrigin"] ?? throw new InvalidOperationException("AllowedServerOrigin not configured."))
						.AllowAnyHeader()
						.AllowAnyMethod()
						.AllowCredentials());
			});

			bool.TryParse(configuration["Jwt:RequireHttpsMetadata"], out bool requireHttpsMetadata);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.MetadataAddress = configuration["Jwt:MetadataAddress"] ?? "";
					options.RequireHttpsMetadata = requireHttpsMetadata;
					options.Audience = configuration["Jwt:Audience"];
					options.MapInboundClaims = false;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = configuration["Jwt:Issuer"],
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						RoleClaimType = "role"
					};
				});

			services.AddScoped<IAuthorizationHandler, ResourceAccessAuthorizationHandler>();
			services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();				
			services.AddAuthorization(options =>
			{
				options.AddPermissionPoliciesForEntities();
			});

			services.AddOpenApi();
			var azureStorageConnectionString = configuration.GetConnectionString("AzureBlobStorage") ?? throw new InvalidOperationException("Connection string 'AzureBlobStorage' not found.");
			var blobServiceClient = new BlobServiceClient(azureStorageConnectionString);
			var properties = blobServiceClient.GetProperties();
			properties.Value.Cors =
				new[]
				{
					new BlobCorsRule
					{
						MaxAgeInSeconds = 1000,
						AllowedHeaders = configuration["Cors:AllowedHeaders"],
						AllowedOrigins = $"{configuration["Cors:AllowedClientOrigin"]}, {configuration["Cors:AllowedServerOrigin"]}",
						ExposedHeaders = configuration["Cors:ExposedHeaders"],
						AllowedMethods = configuration["Cors:AllowedMethods"],
					}
				};
			blobServiceClient.SetProperties(properties);
			services.AddSingleton(x => blobServiceClient);

			services.AddHttpContextAccessor();
			services.AddHttpClient();

			services.AddSingleton<IRolePermissionCacheService, RolePermissionCacheService>();
			services.AddScoped<IUserContextService, UserContextService>();

			return services;
		}

		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			app.UseCors("Client");
			app.UseAuthentication();
			app.UseAuthorization();

			InitializeRolePermissionCache(app).GetAwaiter().GetResult();

			if (isDevelopment)
			{
				app.MapOpenApi();
				app.MapScalarApiReference();
			}
			else
			{
				app.UseHttpsRedirection();
			}
			return app;
		}

		private async Task InitializeRolePermissionCache(WebApplication app)
		{
			try
			{
				Console.WriteLine("Starting role permission cache initialization...");

				using var scope = app.Services.CreateScope();
				var cacheService = scope.ServiceProvider.GetRequiredService<IRolePermissionCacheService>();

				Console.WriteLine("Cache service obtained, calling InitAsync...");
				await cacheService.InitAsync();

				Console.WriteLine("Role permission cache initialized successfully with!");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to initialize role permission cache: {ex.Message}");
				Console.WriteLine($"Exception details: {ex}");
			}
		}

	}
}