using Shared.DTOs.Auth;

namespace ComposedHealthBase.Server.Queries.ModuleQueries
{
    /// <summary>
    /// Interface for retrieving all roles with their associated permissions.
    /// This interface allows loose coupling between the base module and specific module implementations.
    /// </summary>
    public interface IGetAllRolesWithPermissionsQuery
    {
        Task<IEnumerable<RoleDto>> Handle();
    }
}
