using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class DocIdUpadeOnReferral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentStatus",
                schema: "scheduling",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeDocumentId",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "ReferralStatus",
                schema: "scheduling",
                table: "Referrals",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "EmployeeDocumentId", "ReferralStatus" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "AppointmentStatus",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "AppointmentStatus",
                value: 0);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "AppointmentStatus",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentStatus",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "EmployeeDocumentId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "ReferralStatus",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.AddColumn<string>(
                name: "DocumentId",
                schema: "scheduling",
                table: "Referrals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                column: "DocumentId",
                value: "DOC-1001");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                column: "DocumentId",
                value: "DOC-1002");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                column: "DocumentId",
                value: "DOC-1003");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                column: "DocumentId",
                value: "DOC-1004");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                column: "DocumentId",
                value: "DOC-1005");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                column: "DocumentId",
                value: "DOC-1006");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                column: "DocumentId",
                value: "DOC-1007");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DocumentId",
                value: "DOC-1008");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                column: "DocumentId",
                value: "DOC-1009");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DocumentId",
                value: "DOC-1010");
        }
    }
}
