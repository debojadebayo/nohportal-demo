using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddressWarnings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "scheduling",
                table: "Schedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "scheduling",
                table: "Schedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Regular blood pressure monitoring and medication review", "Blood Pressure Check-up" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Review of blood sugar levels and medication adjustment", "Diabetes Follow-up" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Description", "Title" },
                values: new object[] { "Initial evaluation of chronic lower back pain", "Back Pain Assessment" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Title",
                schema: "scheduling",
                table: "Schedules");
        }
    }
}
