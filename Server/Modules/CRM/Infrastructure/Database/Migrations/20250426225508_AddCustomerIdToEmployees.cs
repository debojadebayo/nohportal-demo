using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerIdToEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyName",
                schema: "crm",
                table: "Employees");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CustomerId",
                value: 1L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CustomerId",
                value: 2L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CustomerId",
                value: 3L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CustomerId",
                value: 1L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CustomerId",
                value: 2L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CustomerId",
                value: 3L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CustomerId",
                value: 1L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CustomerId",
                value: 2L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CustomerId",
                value: 3L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CustomerId",
                value: 1L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
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
                column: "CompanyName",
                value: "Acme Corp");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CompanyName",
                value: "Beta Ltd");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CompanyName",
                value: "Gamma Inc");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CompanyName",
                value: "Acme Corp");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CompanyName",
                value: "Beta Ltd");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CompanyName",
                value: "Gamma Inc");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CompanyName",
                value: "Acme Corp");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CompanyName",
                value: "Beta Ltd");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CompanyName",
                value: "Gamma Inc");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CompanyName",
                value: "Acme Corp");
        }
    }
}
