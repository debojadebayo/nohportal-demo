using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Simplify_Document_Metadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerDocuments_Customers_CustomerId",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocuments_Employees_EmployeeId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocuments_EmployeeId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_CustomerDocuments_CustomerId",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentGuid",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "DocumentGuid",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                schema: "crm",
                table: "CustomerDocuments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentGuid",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "crm",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentGuid",
                schema: "crm",
                table: "CustomerDocuments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                schema: "crm",
                table: "CustomerDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_EmployeeId",
                schema: "crm",
                table: "EmployeeDocuments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_CustomerId",
                schema: "crm",
                table: "CustomerDocuments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerDocuments_Customers_CustomerId",
                schema: "crm",
                table: "CustomerDocuments",
                column: "CustomerId",
                principalSchema: "crm",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocuments_Employees_EmployeeId",
                schema: "crm",
                table: "EmployeeDocuments",
                column: "EmployeeId",
                principalSchema: "crm",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
