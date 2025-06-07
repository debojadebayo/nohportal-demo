using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Database;

namespace ComposedHealthBase.Server.Auth.AuthorizationHandlers
{
    public class SubjectOwnedAuthorizationHandler<T, TContext> : AuthorizationHandler<IAuthorizationRequirement, T>
        where TContext : IDbContext<TContext>
        where T : BaseEntity<T>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            IAuthorizationRequirement requirement,
            T resource)
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