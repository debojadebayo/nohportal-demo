using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAuditToStrings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "Customers",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "CustomerDocuments",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "CustomerDocuments",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Contracts",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "crm",
                table: "Contracts",
                type: "text",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { "System", "System" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Customers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Customers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "CustomerDocuments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "CustomerDocuments",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1L, 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0L, 0L });
        }
    }
}
