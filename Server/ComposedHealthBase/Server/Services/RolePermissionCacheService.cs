using System.Collections.Concurrent;
using ComposedHealthBase.Server.Services.Interfaces;
using Shared.DTOs.Auth;
using ComposedHealthBase.Server.Queries.ModuleQueries;
using Microsoft.Extensions.DependencyInjection;

namespace ComposedHealthBase.Server.Services
{
    public class RolePermissionCacheService : IRolePermissionCacheService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ConcurrentDictionary<string, HashSet<string>> _rolePermissionCache = new(StringComparer.OrdinalIgnoreCase);

        public RolePermissionCacheService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task InitAsync()
        {
            try
            {
                // Create a scope to resolve the scoped query service
                using var scope = _serviceProvider.CreateScope();
                var getAllRolesQuery = scope.ServiceProvider.GetRequiredService<IGetAllRolesWithPermissionsQuery>();

                var roles = await getAllRolesQuery.Handle();

                Console.WriteLine($"Loaded {roles.Count()} roles from Auth service");

                _rolePermissionCache.Clear();

                foreach (var role in roles)
                {
                    var permissions = new HashSet<string>(role.Permissions.Select(p => p.Name));
                    _rolePermissionCache.TryAdd(role.Name, permissions);
                }
            }
            catch (Exception ex)
            {
                // Log error but don't throw to prevent application startup issues
                Console.Error.WriteLine($"Failed to initialize role permission cache: {ex.Message}");
            }
        }

        public bool HasPermission(string permissionName, string roleName)
        {
            return _rolePermissionCache.TryGetValue(roleName, out var permissions) &&
                   permissions.Contains(permissionName);
        }
    }
}