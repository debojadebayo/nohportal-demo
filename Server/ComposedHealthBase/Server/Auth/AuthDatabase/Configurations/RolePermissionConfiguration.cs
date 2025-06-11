using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase.Entities;
using Server.ComposedHealthBase.Server.Auth.AuthDatabase;
using ComposedHealthBase.Server.Auth.AuthDatabase.Enums;

namespace ComposedHealthBase.Server.Auth.AuthDatabase.Configurations
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable(nameof(RolePermission), Schema.Auth);

            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        }
    }
}
