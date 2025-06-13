using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Server.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs.CRM;
using System.Reflection;
using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Commands;

namespace ComposedHealthBase.Server.Extensions
{
	public static class ModuleRegistrationExtensions
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration, ref List<Type> moduleTypes, out List<IModule> registeredModules)
		{
			var mapperInterfaceType = typeof(IMapper<,>);

			registeredModules = new List<IModule>();

			// Register open generic query/command handlers for all modules
			services.AddTransient(typeof(GetAllQuery<,,>), typeof(GetAllQuery<,,>));
			services.AddTransient(typeof(GetByIdQuery<,,>), typeof(GetByIdQuery<,,>));
			services.AddTransient(typeof(GetByIdsQuery<,,>), typeof(GetByIdsQuery<,,>));
			services.AddTransient(typeof(GetAllByTenantIdQuery<,,>), typeof(GetAllByTenantIdQuery<,,>));
			services.AddTransient(typeof(GetAllByTenantIdsQuery<,,>), typeof(GetAllByTenantIdsQuery<,,>));
			services.AddTransient(typeof(GetAllBySubjectIdQuery<,,>), typeof(GetAllBySubjectIdQuery<,,>));
			services.AddTransient(typeof(GetAllBySubjectIdsQuery<,,>), typeof(GetAllBySubjectIdsQuery<,,>));
			services.AddTransient(typeof(CreateCommand<,,>), typeof(CreateCommand<,,>));
			services.AddTransient(typeof(UpdateCommand<,,>), typeof(UpdateCommand<,,>));
			services.AddTransient(typeof(DeleteCommand<,>), typeof(DeleteCommand<,>));

			var baseModule = new BaseModule();
			baseModule.RegisterModuleServices(services, configuration);
			registeredModules.Add(baseModule);

			foreach (var moduleType in moduleTypes)
			{
				//Create the module and register the module services
				var module = Activator.CreateInstance(moduleType) as IModule;
				if (module == null)
				{
					throw new InvalidOperationException($"Module type {moduleType.Name} does not implement IModule interface.");
				}
				module.RegisterModuleServices(services, configuration);
				registeredModules.Add(module);

				//Register mappers for each module
				var mapperTypes = moduleType.Assembly.GetTypes()
					.Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
						.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapperInterfaceType))
					.ToList();

				foreach (var mapperType in mapperTypes)
				{
					var interfaceType = mapperType.GetInterfaces()
						.First(i => i.IsGenericType && i.GetGenericTypeDefinition() == mapperInterfaceType);
					services.AddTransient(interfaceType, mapperType);
				}
			}

			return services;
		}
		public static WebApplication ConfigureServicesAndMapEndpoints(this WebApplication app, bool isDevelopment, List<IModule> registeredModules)
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