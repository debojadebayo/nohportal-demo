using Microsoft.AspNetCore.Authorization;

namespace ComposedHealthBase.Server.Auth.Requirements
{
    public class TenantLimitedAdminRequirement : IAuthorizationRequirement
    {
        public TenantLimitedAdminRequirement() { }
    }
}
