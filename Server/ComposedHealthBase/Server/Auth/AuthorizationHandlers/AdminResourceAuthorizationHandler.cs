using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    /// <summary>
    /// Authorization handler that grants access to any resource if the user is in the "administrator" role.
    /// Use this as a top-level handler for global admin override.
    /// </summary>
    public class AdminResourceAuthorizationHandler<TResource> : AuthorizationHandler<IAuthorizationRequirement, TResource>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IAuthorizationRequirement requirement,
            TResource resource)
        {
            if (context.User.IsInRole("administrator"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
