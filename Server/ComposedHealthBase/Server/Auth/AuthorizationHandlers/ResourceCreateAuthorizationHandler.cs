using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using System.Text.Json;
using ComposedHealthBase.Server.Helpers;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    public class ResourceCreateAuthorizationHandler :
        AuthorizationHandler<IAuthorizationRequirement, IAuditEntity>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IAuthorizationRequirement requirement,
            IAuditEntity resource)
        {
            if (context.User.IsInRole("administrator"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else if (context.User.IsInRole("tenantadministrator"))
            {
                var userTenantId = context.User.GetUserTenantId();
                if (!string.IsNullOrEmpty(userTenantId) && resource.TenantId == Guid.Parse(userTenantId))
                {
                    context.Succeed(requirement);
                }
            }
            else if (context.User.IsInRole("subject"))
            {
                var userSubjectId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userSubjectId) && resource.SubjectId == Guid.Parse(userSubjectId))
                {
                    context.Succeed(requirement);
                }
            }

            // if (requirement is AnchorLimitedRequirement && resource is IAnchorable anchorable)
            // {
            //     var userKeycloakId = Guid.Parse(context.User.FindFirstValue("sub") ?? string.Empty);
            //     if (userKeycloakId == Guid.Empty || resource == null)
            //     {
            //         return Task.CompletedTask;
            //     }

            //     // var anchor = await _anchorProvider.GetAnchorByIdAsync(resource.AnchorId);
            //     // if (anchor == null)
            //     // {
            //     //     return;
            //     // }

            //     // if (anchor.CreatedByKeycloakId == userKeycloakId)
            //     // {
            //     //     context.Succeed(requirement);
            //     // }
            // }
            // else

            return Task.CompletedTask;
        }
    }
}