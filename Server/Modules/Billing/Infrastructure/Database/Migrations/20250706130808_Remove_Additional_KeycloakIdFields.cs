using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Billing.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Additional_KeycloakIdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "billing",
                table: "LineItems");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "billing",
                table: "LineItems");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "billing",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "billing",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "billing",
                table: "LineItems",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "billing",
                table: "LineItems",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "billing",
                table: "Invoices",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "billing",
                table: "Invoices",
                newName: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "billing",
                table: "LineItems",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "billing",
                table: "LineItems",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "billing",
                table: "Invoices",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "billing",
                table: "Invoices",
                newName: "SubjectKeycloakId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "billing",
                table: "LineItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "billing",
                table: "LineItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "billing",
                table: "Invoices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "billing",
                table: "Invoices",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
