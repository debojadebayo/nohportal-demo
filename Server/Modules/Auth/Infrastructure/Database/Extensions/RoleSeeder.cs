using Server.Modules.Auth.Infrastructure.Database;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database.Enums;

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
        }
    }
}