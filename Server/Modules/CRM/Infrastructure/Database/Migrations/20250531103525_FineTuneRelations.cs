using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class FineTuneRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.AddColumn<long>(
                name: "ManagerId",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "ManagerId",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "ManagerId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_CustomerId",
                schema: "crm",
                table: "Managers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                schema: "crm",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Managers_ManagerId",
                schema: "crm",
                table: "Employees",
                column: "ManagerId",
                principalSchema: "crm",
                principalTable: "Managers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Managers_Customers_CustomerId",
                schema: "crm",
                table: "Managers",
                column: "CustomerId",
                principalSchema: "crm",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Managers_ManagerId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Managers_Customers_CustomerId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Managers_CustomerId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagerId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "CustomerDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "EmployeeId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CustomerId", "EmployeeId" },
                values: new object[] { 0L, 0L });
        }
    }
}
