using ComposedHealthBase.Server.Auth.AuthDatabase.Enums;
using Microsoft.EntityFrameworkCore;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities;

namespace Server.ComposedHealthBase.Server.Auth.AuthDatabase
{
    public class AuthDbContext : DbContext
    {
        public DbSet<SubjectKeycloakMap> SubjectKeycloakMaps { get; set; }
        public DbSet<TenantKeycloakMap> TenantKeycloakMaps { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.Auth);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}