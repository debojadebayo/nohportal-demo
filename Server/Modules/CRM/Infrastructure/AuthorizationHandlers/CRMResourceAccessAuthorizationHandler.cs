using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Auth.Requirements;
using ComposedHealthBase.Server.Auth.AuthorizationHandlers;
using Server.Modules.CRM.Entities;
using ComposedHealthBase.Server.Auth.Providers;

namespace Server.Modules.CRM.Infrastructure.AuthorizationHandlers
{
    public class CRMResourceAccessRequirement : IAuthorizationRequirement
    {
        public string Description => "User must have access to CRM resources";
    }

    public class CRMResourceAccessAuthorizationHandler :
        AuthorizationHandler<CRMResourceAccessRequirement, IAuditEntity>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            CRMResourceAccessRequirement requirement,
            IAuditEntity resource)
        {
            return Task.CompletedTask;
        }
    }
}
