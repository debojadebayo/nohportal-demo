
namespace ComposedHealthBase.Server.Services
{
    public interface IRolePermissionCacheService
    {
        Task InitAsync();
        bool HasPermission(string permissionName, string roleName);
    }
}