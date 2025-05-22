using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddNotesToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Notes",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Notes",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "crm",
                table: "Employees");
        }
    }
}
