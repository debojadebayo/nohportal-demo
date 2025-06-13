using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Modules.Auth.Infrastructure.Database;
using Server.Modules.Auth.Entities;

namespace Server.Modules.Auth.Infrastructure.Database.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role), Schema.Auth);

            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasData(
                new Role { Id = 1, Name = "Administrator" },
                new Role { Id = 2, Name = "TenantAdministrator" },
                new Role { Id = 3, Name = "TenantLimitedAdministrator" },
                new Role { Id = 4, Name = "Subject" }
            );
        }
    }
}
