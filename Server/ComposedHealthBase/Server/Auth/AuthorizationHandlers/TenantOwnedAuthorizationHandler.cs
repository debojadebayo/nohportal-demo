using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Database;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    public class TenantOwnedRequirement : IAuthorizationRequirement
    {
    }
    public class TenantOwnedAuthorizationHandler : AuthorizationHandler<TenantOwnedRequirement, IEntity>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TenantOwnedRequirement requirement,
            IEntity resource)
        {
            if (context.User.IsInRole("administrator"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            if (context.User.IsInRole("tenantadministrator"))
            {
                var userTenantId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userTenantId) && resource.TenantKeycloakId == Guid.Parse(userTenantId))
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}