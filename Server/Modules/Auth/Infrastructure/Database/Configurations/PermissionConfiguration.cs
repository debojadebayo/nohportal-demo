using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using Server.Modules.Auth.Infrastructure.Database.Enums;

namespace Server.Modules.Auth.Infrastructure.Database.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(nameof(Permission), Schema.Auth);
        }
    }
}
