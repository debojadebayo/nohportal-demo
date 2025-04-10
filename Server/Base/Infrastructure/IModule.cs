using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs.CRM;
using System.Net.NetworkInformation;
using System.Reflection;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure
{
	public interface IModule
	{
		IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration);
		WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment);
	}
	public static class ServiceRegistrationExtensions
	{
		public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration, ref List<Type> moduleTypes, out List<IModule> registeredModules)
		{
			var sr = typeof(IModule);
			registeredModules = new List<IModule>();

			foreach (var module in moduleTypes.Where(x => x.IsAssignableTo(sr) && x.IsClass)
											.Select(Activator.CreateInstance)
											.Cast<IModule>())
			{
				module.RegisterModuleServices(services, configuration);
				registeredModules.Add(module);
			}

			var moduleAssemblies = moduleTypes.Select(x => x.Assembly).ToList();

			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddMaps(moduleAssemblies);
			});
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);

			return services;
		}
		public static WebApplication ConfigureServices(this WebApplication services, bool isDevelopment, ref List<Type> moduleTypes, List<IModule> registeredModules)
		{
			foreach (var module in registeredModules)
			{
				module.ConfigureModuleServices(services, isDevelopment);
			}
			return services;
		}
	}
}