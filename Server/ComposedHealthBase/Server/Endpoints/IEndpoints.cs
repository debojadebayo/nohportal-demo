using ComposedHealthBase.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System.Configuration;
using System.Reflection;

namespace ComposedHealthBase.Server.Endpoints
{
    public interface IEndpoints
    {
        IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints);
    }
}