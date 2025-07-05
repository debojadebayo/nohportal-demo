using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Billing.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Xero_Properties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PostedToXero",
                schema: "billing",
                table: "Invoices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PostedToXeroAt",
                schema: "billing",
                table: "Invoices",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "XeroInvoiceId",
                schema: "billing",
                table: "Invoices",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostedToXero",
                schema: "billing",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PostedToXeroAt",
                schema: "billing",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "XeroInvoiceId",
                schema: "billing",
                table: "Invoices");
        }
    }
}
