using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    public class AnchorLimitedRequirement : IAuthorizationRequirement { }

    public class AnchorLimitedAuthorizationHandler : AuthorizationHandler<AnchorLimitedRequirement, IAnchorable>
    {
        public AnchorLimitedAuthorizationHandler()
        {
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AnchorLimitedRequirement requirement,
            IAnchorable resource)
        {
            if (context.User.IsInRole("administrator"))
            {
                context.Succeed(requirement);
                return;
            }

            var userKeycloakId = Guid.Parse(context.User.FindFirstValue("sub") ?? string.Empty);
            if (userKeycloakId == Guid.Empty || resource == null)
            {
                return;
            }

            // var anchor = await _anchorProvider.GetAnchorByIdAsync(resource.AnchorId);
            // if (anchor == null)
            // {
            //     return;
            // }

            // if (anchor.CreatedByKeycloakId == userKeycloakId)
            // {
            //     context.Succeed(requirement);
            // }
        }
    }
}
