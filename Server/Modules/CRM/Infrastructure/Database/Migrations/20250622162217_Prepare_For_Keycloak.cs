using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Prepare_For_Keycloak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "crm",
                table: "Managers",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "crm",
                table: "Employees",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Domain",
                schema: "crm",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Domain",
                value: "acme.example.com");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202020"),
                column: "Domain",
                value: "nationoh.co.uk");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Domain",
                value: "beta.example.com");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Domain",
                value: "gamma.example.com");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("10101010-dddd-dddd-dddd-101010101010"),
                column: "Username",
                value: "JackWhite");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("11111111-aaaa-aaaa-aaaa-111111111111"),
                column: "Username",
                value: "AliceSmith");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-222222222222"),
                column: "Username",
                value: "BobJones");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("33333333-cccc-cccc-cccc-333333333333"),
                column: "Username",
                value: "CarolWhite");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("44444444-dddd-dddd-dddd-444444444444"),
                column: "Username",
                value: "DavidBlack");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("55555555-eeee-eeee-eeee-555555555555"),
                column: "Username",
                value: "EveGreen");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("66666666-ffff-ffff-ffff-666666666666"),
                column: "Username",
                value: "FrankBlue");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("77777777-aaaa-aaaa-aaaa-777777777777"),
                column: "Username",
                value: "GraceBrown");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("88888888-bbbb-bbbb-bbbb-888888888888"),
                column: "Username",
                value: "HenryGray");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("99999999-cccc-cccc-cccc-999999999999"),
                column: "Username",
                value: "IvyViolet");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-5555-5555-5555-111111111111"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "ThompsonSmith" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-6666-6666-6666-222222222222"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "EmilyJohnson" });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-7777-7777-7777-333333333333"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "MichaelBrown" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Domain",
                schema: "crm",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "crm",
                table: "Managers",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "crm",
                table: "Employees",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("10101010-dddd-dddd-dddd-101010101010"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("11111111-aaaa-aaaa-aaaa-111111111111"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-222222222222"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("33333333-cccc-cccc-cccc-333333333333"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("44444444-dddd-dddd-dddd-444444444444"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("55555555-eeee-eeee-eeee-555555555555"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("66666666-ffff-ffff-ffff-666666666666"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("77777777-aaaa-aaaa-aaaa-777777777777"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("88888888-bbbb-bbbb-bbbb-888888888888"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("99999999-cccc-cccc-cccc-999999999999"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-5555-5555-5555-111111111111"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-6666-6666-6666-222222222222"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-7777-7777-7777-333333333333"),
                column: "UserName",
                value: null);
        }
    }
}
