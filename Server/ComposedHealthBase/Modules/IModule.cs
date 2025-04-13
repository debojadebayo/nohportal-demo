using ComposedHealthBase.Server.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.DTOs.CRM;
using System.Net.NetworkInformation;
using System.Reflection;

namespace ComposedHealthBase.Server.Modules
{
	public interface IModule
	{
		IServiceCollection RegisterModuleServices(IServiceCollection services, IConfiguration configuration);
		WebApplication ConfigureModuleServices(WebApplication app, bool isDevelopment);
	}
}