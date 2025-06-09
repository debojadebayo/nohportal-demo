using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    public class SubjectOwnedRequirement : IAuthorizationRequirement
    {
    }
    public class SubjectOwnedAuthorizationHandler : AuthorizationHandler<SubjectOwnedRequirement, IEntity>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            SubjectOwnedRequirement requirement,
            IEntity resource)
        {
            if (context.User.IsInRole("administrator"))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            var userSubjectId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userSubjectId) && resource.SubjectKeycloakId == Guid.Parse(userSubjectId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}