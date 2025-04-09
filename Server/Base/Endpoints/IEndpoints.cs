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
			foreach (var endpointClass in endpointTypes.Where(x => x.IsAssignableTo(ep) && x.IsClass)
											.Select(Activator.CreateInstance)
											.Cast<IEndpoints>())
			{
				endpointClass.MapEndpoints(app);
			}
			return app;
		}
	}
}