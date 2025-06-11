using ComposedHealthBase.Server.Auth.AuthDatabase.Enums;
using ComposedHealthBase.Server.Modules;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.Extensions
{
    public static class RoleSeeder
    {
        public static async Task SeedRolesAndPermissions(List<IModule> registeredModules, AuthDbContext dbContext)
        {
            foreach (var module in registeredModules)
            {
                var entityTypes = module.GetType().Assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i => i.Name == "IEntity"))
                    .ToList();

                foreach (var entityType in entityTypes)
                {
                    foreach (var permission in Enum.GetValues<PermissionEnum>())
                    {
                        var permissionName = $"{permission}{entityType.Name}";
    
                        var newPermission = new Permission
                        {
                            Name = permissionName
                        };
                        if (dbContext.Permissions.Any(p => p.Name == permissionName))
                        {
                            continue;
                        }
                        await dbContext.Permissions.AddAsync(newPermission);
                    }
                }
            }
            await dbContext.SaveChangesAsync();
        }
    }
}