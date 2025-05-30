using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class productstocustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Contracts_ContractId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ContractId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ContractId",
                schema: "crm",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                schema: "crm",
                table: "Products",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Customers_CustomerId",
                schema: "crm",
                table: "Products",
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
                name: "FK_Products_Customers_CustomerId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomerId",
                schema: "crm",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "ContractId",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ContractId",
                schema: "crm",
                table: "Products",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Contracts_ContractId",
                schema: "crm",
                table: "Products",
                column: "ContractId",
                principalSchema: "crm",
                principalTable: "Contracts",
                principalColumn: "Id");
        }
    }
}
