using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Role_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                schema: "crm",
                table: "Managers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                schema: "crm",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("10101010-dddd-dddd-dddd-101010101010"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("11111111-aaaa-aaaa-aaaa-111111111111"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-222222222222"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("33333333-cccc-cccc-cccc-333333333333"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("44444444-dddd-dddd-dddd-444444444444"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("55555555-eeee-eeee-eeee-555555555555"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("66666666-ffff-ffff-ffff-666666666666"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("77777777-aaaa-aaaa-aaaa-777777777777"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("88888888-bbbb-bbbb-bbbb-888888888888"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("99999999-cccc-cccc-cccc-999999999999"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-5555-5555-5555-111111111111"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-6666-6666-6666-222222222222"),
                column: "RoleName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-7777-7777-7777-333333333333"),
                column: "RoleName",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleName",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "RoleName",
                schema: "crm",
                table: "Employees");
        }
    }
}
