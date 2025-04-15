using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Server.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs.CRM;
using System.Net.NetworkInformation;
using System.Reflection;

namespace ComposedHealthBase.Server.Extensions
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
			}

			var mapperInterfaceType = typeof(IMapper<,>);
			var moduleAssemblies = moduleTypes.Select(t => t.Assembly).Distinct();

			foreach (var assembly in moduleAssemblies)
			{
				var mapperTypes = assembly.GetTypes()
										  .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
											  .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapperInterfaceType));

				foreach (var mapperType in mapperTypes)
				{
					var interfaceType = mapperType.GetInterfaces()
												  .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapperInterfaceType);
					services.AddTransient(interfaceType, mapperType);
				}
			}

			return services;
		}
		public static WebApplication ConfigureServicesAndMapEndpoints(this WebApplication app, bool isDevelopment, ref List<Type> moduleTypes, List<IModule> registeredModules)
		{
			foreach (var module in registeredModules)
			{
				module.ConfigureModuleServices(app, isDevelopment);

				if (module.GetType().Name == "BaseModule")
				{
					continue;
				}

				var endpointAssemblyName = $"Server.Modules.{module.GetType().Name.Replace("Module", "")}.Endpoints";
				var endpointAssembly = Assembly.Load(endpointAssemblyName);

				var endpointTypes = endpointAssembly.GetTypes()
												.Where(x => x.IsAssignableTo(typeof(IEndpoints)) && x.IsClass)
												.Select(Activator.CreateInstance)
												.Cast<IEndpoints>();
				foreach (var endpointType in endpointTypes)
				{
					endpointType.MapEndpoints(app);
				}
			}
			return app;
		}
	}
}