using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contact",
                schema: "crm",
                table: "NOHCustomers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "crm",
                table: "NOHCustomers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "crm",
                table: "NOHCustomers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contact",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "crm",
                table: "NOHCustomers");
        }
    }
}
