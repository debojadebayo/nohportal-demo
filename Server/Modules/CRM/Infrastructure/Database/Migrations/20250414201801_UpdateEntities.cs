using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_NOHCustomerId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "crm",
                table: "Customers",
                newName: "Telephone");

            migrationBuilder.RenameColumn(
                name: "NOHCustomerId",
                schema: "crm",
                table: "Contracts",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_NOHCustomerId",
                schema: "crm",
                table: "Contracts",
                newName: "IX_Contracts_CustomerId");

            migrationBuilder.AddColumn<string>(
                name: "ChargeCode",
                schema: "crm",
                table: "ProductTypes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                schema: "crm",
                table: "ProductTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                schema: "crm",
                table: "ProductTypes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "crm",
                table: "Contracts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Reference",
                schema: "crm",
                table: "Contracts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                schema: "crm",
                table: "Contracts",
                column: "CustomerId",
                principalSchema: "crm",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Customers_CustomerId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ChargeCode",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "EndTime",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "StartTime",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Reference",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "Telephone",
                schema: "crm",
                table: "Customers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                schema: "crm",
                table: "Contracts",
                newName: "NOHCustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_CustomerId",
                schema: "crm",
                table: "Contracts",
                newName: "IX_Contracts_NOHCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Customers_NOHCustomerId",
                schema: "crm",
                table: "Contracts",
                column: "NOHCustomerId",
                principalSchema: "crm",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
