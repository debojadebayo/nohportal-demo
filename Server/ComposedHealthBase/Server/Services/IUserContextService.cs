using System.Security.Claims;

namespace ComposedHealthBase.Server.Services
{
    /// <summary>
    /// Service for accessing user context information from HTTP context
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Gets the subject id ("sub" claim) from the JWT.
        /// </summary>
        string GetSubjectId();

        /// <summary>
        /// Gets the organization id from the custom "organization" claim.
        /// </summary>
        string GetOrganizationId();

        /// <summary>
        /// Gets the user's full name from the claims.
        /// </summary>
        string GetFullName();

        /// <summary>
        /// Gets all roles from the user's claims, including realm_access and resource_access.
        /// </summary>
        List<string> GetRoles();

        /// <summary>
        /// Gets the current user's ClaimsPrincipal
        /// </summary>
        ClaimsPrincipal? GetUser();
    }
}
