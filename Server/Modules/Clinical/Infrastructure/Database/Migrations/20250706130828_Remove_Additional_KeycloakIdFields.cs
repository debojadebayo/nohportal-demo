using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Clinical.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Additional_KeycloakIdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "clinical",
                table: "ClinicalReports");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "clinical",
                table: "ClinicalReports");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "clinical",
                table: "CaseNotes");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "clinical",
                table: "CaseNotes");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "clinical",
                table: "ClinicalReports",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "clinical",
                table: "ClinicalReports",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "clinical",
                table: "CaseNotes",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "clinical",
                table: "CaseNotes",
                newName: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "clinical",
                table: "ClinicalReports",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "clinical",
                table: "ClinicalReports",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "clinical",
                table: "CaseNotes",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "clinical",
                table: "CaseNotes",
                newName: "SubjectKeycloakId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "clinical",
                table: "ClinicalReports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "clinical",
                table: "ClinicalReports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "clinical",
                table: "CaseNotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "clinical",
                table: "CaseNotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
