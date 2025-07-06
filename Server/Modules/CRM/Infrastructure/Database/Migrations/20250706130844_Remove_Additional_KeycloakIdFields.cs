using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Additional_KeycloakIdFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "ProductTypes",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "ProductTypes",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Products",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Products",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Managers",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Managers",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Employees",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Employees",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Customers",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Customers",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "CustomerDocuments",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "CustomerDocuments",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Contracts",
                newName: "ModifiedById");

            migrationBuilder.RenameColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Contracts",
                newName: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "ProductTypes",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "ProductTypes",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "Products",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "Products",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "Managers",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "Managers",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "Employees",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "Employees",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "EmployeeDocuments",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "EmployeeDocuments",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "Customers",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "Customers",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "CustomerDocuments",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "CustomerDocuments",
                newName: "SubjectKeycloakId");

            migrationBuilder.RenameColumn(
                name: "ModifiedById",
                schema: "crm",
                table: "Contracts",
                newName: "TenantKeycloakId");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                schema: "crm",
                table: "Contracts",
                newName: "SubjectKeycloakId");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "ProductTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "ProductTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Contracts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Contracts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202020"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("10101010-dddd-dddd-dddd-101010101010"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("11111111-aaaa-aaaa-aaaa-111111111111"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-222222222222"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("33333333-cccc-cccc-cccc-333333333333"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("44444444-dddd-dddd-dddd-444444444444"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("55555555-eeee-eeee-eeee-555555555555"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("66666666-ffff-ffff-ffff-666666666666"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("77777777-aaaa-aaaa-aaaa-777777777777"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("88888888-bbbb-bbbb-bbbb-888888888888"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("99999999-cccc-cccc-cccc-999999999999"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-5555-5555-5555-111111111111"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-6666-6666-6666-222222222222"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-7777-7777-7777-333333333333"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111114"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111115"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111116"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111117"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111118"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111119"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111120"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111121"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111122"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111123"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111124"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111125"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111126"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111127"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111128"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111129"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
