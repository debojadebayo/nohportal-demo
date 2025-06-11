using ComposedHealthBase.Server.Auth.AuthDatabase.Enums;
using ComposedHealthBase.Server.Modules;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.Extensions
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAndPermissions(AuthDbContext dbContext)
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
                    await dbContext.Permissions.AddAsync(newPermission);
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}