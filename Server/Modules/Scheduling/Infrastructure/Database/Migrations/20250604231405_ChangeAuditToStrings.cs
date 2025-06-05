using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAuditToStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });
        }
    }
}
