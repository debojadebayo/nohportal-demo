using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Auth.Entities;

namespace Server.Modules.Auth.Infrastructure.Database
{
    public sealed class AuthDbContext(DbContextOptions<AuthDbContext> options) : BaseDbContext<AuthDbContext>(options), IDbContext<AuthDbContext>
    {
        public DbSet<SubjectKeycloakMap> SubjectKeycloakMaps { get; set; }
        public DbSet<TenantKeycloakMap> TenantKeycloakMaps { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<LocalStorageKey> LocalStorageKeys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.Auth);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);

            modelBuilder.Entity<Permission>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Role>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<SubjectKeycloakMap>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<TenantKeycloakMap>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<LocalStorageKey>()
                .Property(p => p.Id)
                .HasDefaultValueSql("gen_random_uuid()");
        }
    }
}