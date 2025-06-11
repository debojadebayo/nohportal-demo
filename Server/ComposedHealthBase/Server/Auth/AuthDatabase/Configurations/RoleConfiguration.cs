using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities;

namespace ComposedHealthBase.Server.Auth.AuthDatabase.Configurations
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

            builder.HasData(Role.GetValues());
        }
    }
}
