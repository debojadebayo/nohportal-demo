using Microsoft.AspNetCore.Authorization;

namespace ComposedHealthBase.Server.Auth.Requirements
{
    /// <summary>
    /// Authorization requirement that requires a specific permission
    /// </summary>
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// The permission string (e.g., "CreateSchedule", "ViewEmployee")
        /// </summary>
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
        }
    }
}
