using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Auth.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_LocalStorage_Keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalStorageKeys",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ObjectTypeName = table.Column<string>(type: "text", nullable: false),
                    ObjectGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    PrivateKey = table.Column<string>(type: "text", nullable: false),
                    PublicKey = table.Column<string>(type: "text", nullable: false),
                    KeyGeneratedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    KeyExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalStorageKeys", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalStorageKeys",
                schema: "auth");
        }
    }
}
