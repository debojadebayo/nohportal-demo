using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddProductIdStatusToSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "scheduling",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "scheduling",
                table: "Schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ProductId", "Status" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ProductId", "Status" },
                values: new object[] { 0L, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ProductId", "Status" },
                values: new object[] { 0L, 0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "scheduling",
                table: "Schedules");
        }
    }
}
