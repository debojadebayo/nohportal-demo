using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using Server.Modules.Auth.Infrastructure.Database.Enums;

namespace Server.Modules.Auth.Infrastructure.Database.Configurations
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
