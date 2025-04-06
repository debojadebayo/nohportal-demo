using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ComposedHealthBase.Server.BaseModule.Endpoints
{
	public class BaseEndpoints : IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var group = endpoints.MapGroup("/api/Base");

			return endpoints;
		}

		
	}
}