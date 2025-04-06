using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Reflection;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure
{
	public class BaseModule : IModule
	{
		public IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDatabaseDeveloperPageExceptionFilter();
			services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
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
			services.AddControllers();
			services.AddOpenApi();

			return services;
		}

		public WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment)
		{
			app.UseHttpsRedirection();
			app.UseCors();
			app.UseAuthentication();
			app.UseAuthorization();
			if (isDevelopment)
			{
				app.MapOpenApi();
				app.MapScalarApiReference();
				app.UseHttpsRedirection();
			}
			return app;
		}
	}
}