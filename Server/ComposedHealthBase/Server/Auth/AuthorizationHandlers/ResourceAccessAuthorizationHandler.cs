using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using System.Text.Json;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    public class ResourceAccessAuthorizationHandler :
        AuthorizationHandler<IAuthorizationRequirement, IEntity>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IAuthorizationRequirement requirement,
            IEntity resource)
        {
            if (context.User.IsInRole("administrator"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
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
            if (requirement is SubjectOwnedRequirement)
            {
                var userSubjectId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userSubjectId) && resource.SubjectKeycloakId == Guid.Parse(userSubjectId))
                {
                    context.Succeed(requirement);
                }
            }
            else if (requirement is TenantOwnedRequirement)
            {
                var orgClaim = context.User.FindFirst("organization")?.Value;
                var userTenantId = string.Empty;
                if (!string.IsNullOrEmpty(orgClaim))
                {
                    var orgArray = JsonDocument.Parse(orgClaim).RootElement.EnumerateArray();
                    foreach (var orgObj in orgArray)
                    {
                        foreach (var org in orgObj.EnumerateObject())
                        {
                            userTenantId = org.Value.GetProperty("id").GetString();
                        }
                    }
                }
                if (!string.IsNullOrEmpty(userTenantId) && resource.TenantKeycloakId == Guid.Parse(userTenantId))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}