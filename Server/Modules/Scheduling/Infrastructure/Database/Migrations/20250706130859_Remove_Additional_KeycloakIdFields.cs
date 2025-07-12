using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Additional_KeycloakIdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "scheduling",
                table: "Schedules",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Schedules",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "scheduling",
                table: "Referrals",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Referrals",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "scheduling",
                table: "Clinicians",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Clinicians",
                newName: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "scheduling",
                table: "Schedules",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "scheduling",
                table: "Schedules",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "scheduling",
                table: "Referrals",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "scheduling",
                table: "Referrals",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "scheduling",
                table: "Clinicians",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "scheduling",
                table: "Clinicians",
                newName: "SubjectKeycloakId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "scheduling",
                table: "Schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Referrals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "scheduling",
                table: "Referrals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Clinicians",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "scheduling",
                table: "Clinicians",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
