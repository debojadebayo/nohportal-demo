
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using ComposedHealthBase.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
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
					options.MetadataAddress = configuration["Jwt:MetadataAddress"];
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

			services.AddAuthorization(options =>
			{
				options.AddPolicy("administrator", policy => policy.RequireRole("administrator"));
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

			services.AddTransient<IUserContextService, UserContextService>();

			return services;
		}

		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			app.UseCors("Client");
			app.UseAuthentication();
			app.UseAuthorization();
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
	}
}