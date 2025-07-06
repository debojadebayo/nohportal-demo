using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Auth.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Additional_KeycloakIdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "auth",
                table: "LocalStorageKeys");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "auth",
                table: "LocalStorageKeys");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "auth",
                table: "LocalStorageKeys",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "auth",
                table: "LocalStorageKeys",
                newName: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "auth",
                table: "LocalStorageKeys",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "auth",
                table: "LocalStorageKeys",
                newName: "SubjectKeycloakId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "auth",
                table: "LocalStorageKeys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "auth",
                table: "LocalStorageKeys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
