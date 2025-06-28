using Microsoft.AspNetCore.Authorization;

namespace ComposedHealthBase.Server.Auth.Attributes
{
    /// <summary>
    /// Authorization attribute that requires a specific permission
    /// </summary>
    public class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(string permission) : base($"Permission:{permission}")
        {
        }
    }
}
