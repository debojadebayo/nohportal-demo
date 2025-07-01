using Server.Modules.Auth.Infrastructure.Database;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database.Enums;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Auth.Infrastructure.Database.Extensions
{
    public static class RoleSeeder
    {
        public static void SeedRolesAndPermissions(AuthDbContext dbContext)
        {
            Console.WriteLine("Seeding roles and permissions...");

            // Scan all loaded assemblies for IEntity implementations
            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == "IEntity"))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                Console.WriteLine($"Seeding roles and permissions for {entityType.Name}...");
                foreach (var permission in Enum.GetValues<PermissionEnum>())
                {
                    var permissionName = $"{permission}{entityType.Name}";

                    if (dbContext.Permissions.Any(p => p.Name == permissionName))
                    {
                        continue;
                    }
                    var newPermission = new Permission
                    {
                        Name = permissionName
                    };
                    dbContext.Permissions.Add(newPermission);
                }
            }
            dbContext.SaveChanges();

            // Seed Administrator role with Role-specific permissions
            SeedAdministratorRole(dbContext);
        }

        private static void SeedAdministratorRole(AuthDbContext dbContext)
        {
            Console.WriteLine("Seeding Administrator role...");

            const string adminRoleName = "Administrator";
            
            // Check if Administrator role already exists, create if it doesn't
            var adminRole = dbContext.Roles
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Name == adminRoleName);
                
            if (adminRole == null)
            {
                adminRole = new Role
                {
                    Name = adminRoleName,
                    Permissions = new List<Permission>()
                };
                dbContext.Roles.Add(adminRole);
                dbContext.SaveChanges(); // Save to get the role ID
                Console.WriteLine($"Created {adminRoleName} role");
            }

            // Define the Role-specific permissions that Administrator should have
            var rolePermissionNames = new[]
            {
                "ViewRole",
                "CreateRole",
                "UpdateRole",
                "DeleteRole",
                "ViewPermission",
                "CreatePermission",
                "UpdatePermission",
                "DeletePermission"
            };

            // Get the Role permissions from the database
            var rolePermissions = dbContext.Permissions
                .Where(p => rolePermissionNames.Contains(p.Name))
                .ToList();

            // Add missing permissions to Administrator role
            foreach (var permission in rolePermissions)
            {
                if (!adminRole.Permissions.Any(p => p.Id == permission.Id))
                {
                    adminRole.Permissions.Add(permission);
                    Console.WriteLine($"Added {permission.Name} permission to {adminRoleName} role");
                }
            }

            dbContext.SaveChanges();
            Console.WriteLine($"Administrator role seeding completed with {rolePermissions.Count} Role permissions");
        }
    }
}