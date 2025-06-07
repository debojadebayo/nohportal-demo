using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class HarmoniseUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Referrals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
                schema: "scheduling",
                table: "Referrals",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "AvatarTitle",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarImage",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarDescription",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Clinicians",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "KeycloakId",
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

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Clinicians",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
                schema: "scheduling",
                table: "Clinicians",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("9c8b7a6d-5e4f-3c2b-1a09-876543210fed"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("123e4567-e89b-12d3-a456-426614174000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ba012345-6789-abcd-0123-456789abcdef"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00112233-4455-6677-8899-aabbccddeeff"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("ffeeddcc-bbaa-9988-7766-554433221100"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("abcdef01-2345-6789-abcd-ef0123456789"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("fedcba98-7654-3210-fedc-ba9876543210"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedByKeycloakId", "KeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "KeycloakId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "SubjectKeycloakId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.AlterColumn<string>(
                name: "AvatarTitle",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarImage",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AvatarDescription",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
