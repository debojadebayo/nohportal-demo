using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentEnumAndSwitchToGuids : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentGuid",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "EmployeeDocumentType",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerDocumentType",
                schema: "crm",
                table: "CustomerDocuments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentGuid",
                schema: "crm",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentGuid",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "EmployeeDocumentType",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "CustomerDocumentType",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentGuid",
                schema: "crm",
                table: "CustomerDocuments");
        }
    }
}
