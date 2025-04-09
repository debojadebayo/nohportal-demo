using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database
{
    /// <inheritdoc />
    public partial class EntitiesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NOHCustomerProduct",
                schema: "crm");

            migrationBuilder.RenameColumn(
                name: "TownOrCity",
                schema: "crm",
                table: "NOHCustomers",
                newName: "Website");

            migrationBuilder.RenameColumn(
                name: "Street",
                schema: "crm",
                table: "NOHCustomers",
                newName: "Site");

            migrationBuilder.RenameColumn(
                name: "HouseNumberOrName",
                schema: "crm",
                table: "NOHCustomers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "County",
                schema: "crm",
                table: "NOHCustomers",
                newName: "OHServicesRequired");

            migrationBuilder.RenameColumn(
                name: "Country",
                schema: "crm",
                table: "NOHCustomers",
                newName: "InvoiceEmail");

            migrationBuilder.RenameColumn(
                name: "Contact",
                schema: "crm",
                table: "NOHCustomers",
                newName: "Industry");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "crm",
                table: "ProductTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "crm",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                schema: "crm",
                table: "NOHCustomers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "NOHCustomers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "NOHCustomers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "crm",
                table: "NOHCustomers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfEmployees",
                schema: "crm",
                table: "NOHCustomers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                schema: "crm",
                table: "NOHCustomers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Contracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "crm",
                table: "Contracts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Contracts",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_NOHCustomers_ProductId",
                schema: "crm",
                table: "NOHCustomers",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_NOHCustomers_Products_ProductId",
                schema: "crm",
                table: "NOHCustomers",
                column: "ProductId",
                principalSchema: "crm",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NOHCustomers_Products_ProductId",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropIndex(
                name: "IX_NOHCustomers_ProductId",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Address",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "NumberOfEmployees",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "ProductId",
                schema: "crm",
                table: "NOHCustomers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.RenameColumn(
                name: "Website",
                schema: "crm",
                table: "NOHCustomers",
                newName: "TownOrCity");

            migrationBuilder.RenameColumn(
                name: "Site",
                schema: "crm",
                table: "NOHCustomers",
                newName: "Street");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                schema: "crm",
                table: "NOHCustomers",
                newName: "HouseNumberOrName");

            migrationBuilder.RenameColumn(
                name: "OHServicesRequired",
                schema: "crm",
                table: "NOHCustomers",
                newName: "County");

            migrationBuilder.RenameColumn(
                name: "InvoiceEmail",
                schema: "crm",
                table: "NOHCustomers",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "Industry",
                schema: "crm",
                table: "NOHCustomers",
                newName: "Contact");

            migrationBuilder.CreateTable(
                name: "NOHCustomerProduct",
                schema: "crm",
                columns: table => new
                {
                    NOHCustomersId = table.Column<long>(type: "bigint", nullable: false),
                    ProductsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NOHCustomerProduct", x => new { x.NOHCustomersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_NOHCustomerProduct_NOHCustomers_NOHCustomersId",
                        column: x => x.NOHCustomersId,
                        principalSchema: "crm",
                        principalTable: "NOHCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NOHCustomerProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "crm",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NOHCustomerProduct_ProductsId",
                schema: "crm",
                table: "NOHCustomerProduct",
                column: "ProductsId");
        }
    }
}
