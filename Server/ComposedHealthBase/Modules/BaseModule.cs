
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
						.WithOrigins("http://localhost:5002")
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
			services.AddAntiforgery();
			services.AddOpenApi();

			return services;
		}

		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			app.UseCors("Client");
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseAntiforgery();
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