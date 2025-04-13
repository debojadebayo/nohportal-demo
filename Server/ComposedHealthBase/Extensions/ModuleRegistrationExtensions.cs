using ComposedHealthBase.Server.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs.CRM;
using System.Net.NetworkInformation;
using System.Reflection;

namespace ComposedHealthBase.Extensions
{
    public static class ModuleRegistrationExtensions
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration, ref List<Type> moduleTypes, out List<IModule> registeredModules)
		{
			registeredModules = new List<IModule>();

			foreach (var module in moduleTypes.Where(x => x.IsAssignableTo(typeof(IModule)) && x.IsClass)
											.Select(Activator.CreateInstance)
											.Cast<IModule>())
			{
				module.RegisterModuleServices(services, configuration);
				registeredModules.Add(module);

				var mapperTypes = module.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IMapper)) && x.IsClass)
											.Select(Activator.CreateInstance)
											.Cast<IMapper>();

				foreach (var mapper in mapperTypes)
				{
					var entityType = mapper.GetType().GetInterfaces()
						.FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapper<,>));

					if (entityType != null)
					{
						var sourceType = entityType.GenericTypeArguments[0];
						var destinationType = entityType.GenericTypeArguments[1];

						services.AddScoped(typeof(IMapper<,>).MakeGenericType(sourceType, destinationType), mapper);
					}
				}
			}

			return services;
		}
		public static WebApplication ConfigureServicesAndMapEndpoints(this WebApplication app, bool isDevelopment, ref List<Type> moduleTypes, List<IModule> registeredModules)
		{
			foreach (var module in registeredModules)
			{
				module.ConfigureModuleServices(app, isDevelopment);

				var endpointTypes = module.GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(IEndpoints)) && x.IsClass)
							.Select(Activator.CreateInstance)
							.Cast<IEndpoints>();

				foreach (var endpointClass in endpointTypes)
				{
					endpointClass.MapEndpoints(app);
				}
			}
			return app;
		}
	}
}