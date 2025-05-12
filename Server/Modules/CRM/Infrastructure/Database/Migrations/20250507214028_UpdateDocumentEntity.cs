using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDocumentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Customers_CustomerId",
                schema: "crm",
                table: "Documents");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EmployeeId",
                schema: "crm",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "crm",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Customers_CustomerId",
                schema: "crm",
                table: "Documents",
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
                name: "FK_Documents_Customers_CustomerId",
                schema: "crm",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                schema: "crm",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "crm",
                table: "Documents");

            migrationBuilder.AlterColumn<long>(
                name: "CustomerId",
                schema: "crm",
                table: "Documents",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Customers_CustomerId",
                schema: "crm",
                table: "Documents",
                column: "CustomerId",
                principalSchema: "crm",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
