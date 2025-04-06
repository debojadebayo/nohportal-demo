using ComposedHealthBase.Server.BaseModule.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Configuration;
using System.Reflection;

namespace ComposedHealthBase.Server.BaseModule.Endpoints
{
	public interface IEndpoints
	{
		IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
	}
	public static class EndpointMappingExtensions
	{
		public static WebApplication MapEndpoints(this WebApplication app, ref List<Type> endpointTypes)
		{
			var ep = typeof(IEndpoints);
			string path = AppDomain.CurrentDomain.BaseDirectory;
			foreach (var file in Directory.GetFiles(path, "*.dll"))
			{
				try
				{
					var assembly = Assembly.LoadFrom(file);
					foreach (var endpointClass in assembly.GetTypes()
						.Where(x => x.IsAssignableTo(ep) && x.IsClass)
						.Select(Activator.CreateInstance)
						.Cast<IEndpoints>())
					{
						endpointClass.MapEndpoints(app);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Error loading assembly: {file}, {ex.Message}");
				}
			}
			return app;
		}
	}
}