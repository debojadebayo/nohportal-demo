using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FineTuneRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ReferralId",
                schema: "scheduling",
                table: "Schedules",
                column: "ReferralId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Referrals_ReferralId",
                schema: "scheduling",
                table: "Schedules",
                column: "ReferralId",
                principalSchema: "scheduling",
                principalTable: "Referrals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Referrals_ReferralId",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_ReferralId",
                schema: "scheduling",
                table: "Schedules");

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
        }
    }
}
