using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class TenantSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 2L, 1L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 3L, 1L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 4L, 2L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 5L, 2L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 6L, 2L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 7L, 3L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 8L, 3L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 9L, 3L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 10L, 3L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 2L, 1L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 3L, 1L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L });
        }
    }
}
