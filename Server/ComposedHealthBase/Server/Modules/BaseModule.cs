
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
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
					options.Authority = configuration["Jwt:Issuer"];
					options.Audience = configuration["Jwt:Audience"];
					options.RequireHttpsMetadata = requireHttpsMetadata;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true
					};
				});

			services.AddAuthorization();
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