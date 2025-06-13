using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using System.Text.Json;
using ComposedHealthBase.Server.Helpers;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    public class ResourceAccessAuthorizationHandler :
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
            }else if (context.User.IsInRole("tenantadministrator"))
            {
                var userTenantId = context.User.GetUserTenantId();
                if (!string.IsNullOrEmpty(userTenantId) && resource.TenantKeycloakId == Guid.Parse(userTenantId))
                {
                    context.Succeed(requirement);
                }
            }else if (context.User.IsInRole("subject"))
            {
                var userSubjectId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(userSubjectId) && resource.SubjectKeycloakId == Guid.Parse(userSubjectId))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}