using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class TenantSubject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                schema: "crm",
                table: "Contracts");

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
                name: "CustomerId",
                schema: "crm",
                table: "Products",
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
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<Guid>(
                name: "KeycloakId",
                schema: "crm",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "Customers",
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

            migrationBuilder.AddColumn<Guid>(
                name: "KeycloakId",
                schema: "crm",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
                columns: new[] { "CustomerId", "EmployeeId", "KeycloakId" },
                values: new object[] { 0L, 0L, new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CustomerId", "EmployeeId", "KeycloakId" },
                values: new object[] { 0L, 0L, new Guid("22222222-2222-2222-2222-222222222222") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CustomerId", "EmployeeId", "KeycloakId" },
                values: new object[] { 0L, 0L, new Guid("33333333-3333-3333-3333-333333333333") });

            migrationBuilder.InsertData(
                schema: "crm",
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedDate", "CustomerId", "Email", "EmployeeId", "Industry", "InvoiceEmail", "IsActive", "KeycloakId", "LastModifiedBy", "ModifiedDate", "Name", "Notes", "NumberOfEmployees", "OHServicesRequired", "Postcode", "Site", "SubjectId", "Telephone", "TenantId", "Website" },
                values: new object[] { 20L, "First Floor, Swan Buildings, 20 Swan Street, Manchester", 1L, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), 0L, "contact@nationoh.co.uk", 0L, "Occupatioanl Health", "contact@nationoh.co.uk", true, new Guid("44444444-4444-4444-4444-444444444444"), 1L, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Nation Occupational Health", "", 200, "Ad hoc assessments", "M4 5JW", "Birmingham", 0L, "01147 004 362", 0L, "https://www.nationoh.co.uk" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), 2L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), 3L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), 2L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), 3L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), 1L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), 2L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("33333333-cccc-cccc-cccc-333333333333"), 3L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "EmployeeId", "KeycloakId", "TenantId" },
                values: new object[] { 0L, new Guid("44444444-dddd-dddd-dddd-444444444444"), 1L });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                schema: "crm",
                table: "Contracts",
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
                name: "FK_Contracts_Customers_CustomerId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "KeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "KeycloakId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "TenantId",
                value: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                schema: "crm",
                table: "Contracts",
                column: "CustomerId",
                principalSchema: "crm",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
