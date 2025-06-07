using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class HarmoniseUserTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                schema: "crm",
                table: "Managers",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "crm",
                table: "Managers",
                newName: "LastName");

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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "ProductTypes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "AvatarDescription",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarImage",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarTitle",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarDescription",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarImage",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarTitle",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: true);

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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: true);

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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
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

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Contracts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Contracts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "FirstName", "LastName", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), "Thompson", "Smith", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "FirstName", "LastName", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), "Emily", "Johnson", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedByKeycloakId", "FirstName", "LastName", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId", "UserName" },
                values: new object[] { null, null, null, new Guid("00000000-0000-0000-0000-000000000000"), "Michael", "Brown", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedByKeycloakId", "ModifiedByKeycloakId", "SubjectKeycloakId", "TenantKeycloakId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AvatarDescription",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "AvatarImage",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "AvatarTitle",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "AvatarDescription",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AvatarImage",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AvatarTitle",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedByKeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ModifiedByKeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserName",
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
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
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
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

            migrationBuilder.DropColumn(
                name: "SubjectKeycloakId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "TenantKeycloakId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                schema: "crm",
                table: "Managers",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "crm",
                table: "Managers",
                newName: "Name");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Thompson Smith");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "Emily Johnson");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "Michael Brown");
        }
    }
}
