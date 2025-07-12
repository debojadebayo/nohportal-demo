using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ComposedHealthBase.Server.Services
{
    /// <summary>
    /// Service for accessing user context information from HTTP context
    /// </summary>
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _context;

        public UserContextService(IHttpContextAccessor context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the subject id ("sub" claim) from the JWT.
        /// </summary>
        public string GetSubjectId()
        {
            return _context.HttpContext?.User.FindFirst("sub")?.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets the organization id from the custom "organization" claim.
        /// </summary>
        public string GetOrganizationId()
        {
            var orgClaim = _context.HttpContext?.User.FindFirst("organization")?.Value;
            if (string.IsNullOrEmpty(orgClaim))
                return string.Empty;

            try
            {
                // The claim is a JSON array of objects, e.g.:
                using var doc = JsonDocument.Parse(orgClaim);
                var root = doc.RootElement;
                if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 0)
                {
                    var firstObj = root[0];
                    if (firstObj.TryGetProperty("NationOH", out var nationOhObj))
                    {
                        if (nationOhObj.TryGetProperty("id", out var idProp))
                        {
                            return idProp.GetString() ?? string.Empty;
                        }
                    }

                    // Fallback: try to get id property directly from first object
                    if (firstObj.TryGetProperty("id", out var directIdProp))
                    {
                        return directIdProp.GetString() ?? string.Empty;
                    }
                }
            }
            catch
            {
                // Ignore parse errors and return empty string
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the user's full name from the claims.
        /// </summary>
        public string GetFullName()
        {
            var firstName = _context.HttpContext?.User.FindFirst("given_name")?.Value ?? string.Empty;
            var lastName = _context.HttpContext?.User.FindFirst("family_name")?.Value ?? string.Empty;
            return $"{firstName} {lastName}".Trim();
        }

        /// <summary>
        /// Gets all roles from the user's claims, including realm_access and resource_access.
        /// </summary>
        public List<string> GetRoles()
        {
            var roles = new List<string>();
            var user = _context.HttpContext?.User;

            if (user == null) return roles;

            // Get roles from standard role claims
            var roleClaims = user.FindAll("role").Select(c => c.Value);
            roles.AddRange(roleClaims);

            // Get roles from realm_access
            var realmAccessClaim = user.FindFirst("realm_access")?.Value;
            if (!string.IsNullOrEmpty(realmAccessClaim))
            {
                try
                {
                    using var doc = JsonDocument.Parse(realmAccessClaim);
                    if (doc.RootElement.TryGetProperty("roles", out var realmRoles) &&
                        realmRoles.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var role in realmRoles.EnumerateArray())
                        {
                            var roleValue = role.GetString();
                            if (!string.IsNullOrEmpty(roleValue) && !roles.Contains(roleValue))
                            {
                                roles.Add(roleValue);
                            }
                        }
                    }
                }
                catch
                {
                    // Ignore parse errors
                }
            }

            // Get roles from resource_access
            var resourceAccessClaim = user.FindFirst("resource_access")?.Value;
            if (!string.IsNullOrEmpty(resourceAccessClaim))
            {
                try
                {
                    using var doc = JsonDocument.Parse(resourceAccessClaim);
                    foreach (var resource in doc.RootElement.EnumerateObject())
                    {
                        if (resource.Value.TryGetProperty("roles", out var resourceRoles) &&
                            resourceRoles.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var role in resourceRoles.EnumerateArray())
                            {
                                var roleValue = role.GetString();
                                if (!string.IsNullOrEmpty(roleValue) && !roles.Contains(roleValue))
                                {
                                    roles.Add(roleValue);
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Ignore parse errors
                }
            }

            return roles.Distinct().ToList();
        }

        /// <summary>
        /// Gets the current user's ClaimsPrincipal
        /// </summary>
        public ClaimsPrincipal? GetUser()
        {
            return _context.HttpContext?.User;
        }
    }
}
