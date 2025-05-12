using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangePatientToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatientId",
                schema: "scheduling",
                table: "Schedules",
                newName: "EmployeeId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                schema: "scheduling",
                table: "Referrals",
                newName: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                schema: "scheduling",
                table: "Schedules",
                newName: "PatientId");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                schema: "scheduling",
                table: "Referrals",
                newName: "PatientId");
        }
    }
}
