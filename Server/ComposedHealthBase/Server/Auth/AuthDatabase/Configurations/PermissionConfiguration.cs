using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase;
using ComposedHealthBase.Server.Auth.AuthDatabase.Enums;

namespace ComposedHealthBase.Server.Auth.AuthDatabase.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(nameof(Permission), Schema.Auth);
        }
    }
}
