using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchTagsToAllRelevantEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchTags",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchTags",
                schema: "crm",
                table: "EmployeeDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SearchTags",
                schema: "crm",
                table: "CustomerDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SearchTags",
                value: "1 Acme Corp 1 Acme Street London AC1 2ME 01234 567890");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SearchTags",
                value: "2 Beta Ltd 2 Beta Road Manchester BT2 3LT 02345 678901");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20L,
                column: "SearchTags",
                value: "20 Nation Occupational Health First Floor Swan Buildings 20 Swan Street Manchester M4 5JW 01147 004 362");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SearchTags",
                value: "1 Alice Smith 1 Main St Apt 1 EMP1 1AA 07111 111111");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SearchTags",
                value: "2 Bob Jones 2 Main St Apt 2 EMP2 2BB 07222 222222");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SearchTags",
                value: "3 Carol White 3 Main St Apt 3 EMP3 3CC 07333 333333");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "SearchTags",
                value: "4 David Black 4 Main St Apt 4 EMP4 4DD 07444 444444");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "SearchTags",
                value: "5 Eve Green 5 Main St Apt 5 EMP5 5EE 07555 555555");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "SearchTags",
                value: "6 Frank Blue 6 Main St Apt 6 EMP6 6FF 07666 666666");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "SearchTags",
                value: "7 Grace Brown 7 Main St Apt 7 EMP7 7GG 07777 777777");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "SearchTags",
                value: "8 Henry Gray 8 Main St Apt 8 EMP8 8HH 07888 888888");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "SearchTags",
                value: "9 Ivy Violet 9 Main St Apt 9 EMP9 9II 07999 999999");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "SearchTags",
                value: "10 Jack White 10 Main St Apt 10 EMP10 0JJ 07000 000000");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SearchTags",
                value: "1 Thompson Smith 1 Acme Street London AC1 2ME 07111 111111");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SearchTags",
                value: "2 Emily Johnson 2 Beta Road Manchester BT2 3LT 07222 222222");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SearchTags",
                value: "3 Michael Brown 3 Gamma Avenue Birmingham GM3 4IN 07333 333333");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchTags",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "SearchTags",
                schema: "crm",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "SearchTags",
                schema: "crm",
                table: "CustomerDocuments");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 20L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                column: "SearchTags",
                value: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                column: "SearchTags",
                value: "");
        }
    }
}
