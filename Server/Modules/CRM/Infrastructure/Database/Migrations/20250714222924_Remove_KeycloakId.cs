using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Remove_KeycloakId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeycloakId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "KeycloakId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "KeycloakId",
                schema: "crm",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "KeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "KeycloakId",
                schema: "crm",
                table: "Employees",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "KeycloakId",
                schema: "crm",
                table: "Customers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "KeycloakId",
                value: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202020"),
                column: "KeycloakId",
                value: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "KeycloakId",
                value: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "KeycloakId",
                value: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("10101010-dddd-dddd-dddd-101010101010"),
                column: "KeycloakId",
                value: new Guid("44444444-dddd-dddd-dddd-444444444444"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("11111111-aaaa-aaaa-aaaa-111111111111"),
                column: "KeycloakId",
                value: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("22222222-bbbb-bbbb-bbbb-222222222222"),
                column: "KeycloakId",
                value: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("33333333-cccc-cccc-cccc-333333333333"),
                column: "KeycloakId",
                value: new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("44444444-dddd-dddd-dddd-444444444444"),
                column: "KeycloakId",
                value: new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("55555555-eeee-eeee-eeee-555555555555"),
                column: "KeycloakId",
                value: new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("66666666-ffff-ffff-ffff-666666666666"),
                column: "KeycloakId",
                value: new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("77777777-aaaa-aaaa-aaaa-777777777777"),
                column: "KeycloakId",
                value: new Guid("11111111-aaaa-aaaa-aaaa-111111111111"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("88888888-bbbb-bbbb-bbbb-888888888888"),
                column: "KeycloakId",
                value: new Guid("22222222-bbbb-bbbb-bbbb-222222222222"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("99999999-cccc-cccc-cccc-999999999999"),
                column: "KeycloakId",
                value: new Guid("33333333-cccc-cccc-cccc-333333333333"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-5555-5555-5555-111111111111"),
                column: "KeycloakId",
                value: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-6666-6666-6666-222222222222"),
                column: "KeycloakId",
                value: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: new Guid("33333333-7777-7777-7777-333333333333"),
                column: "KeycloakId",
                value: new Guid("77777777-7777-7777-7777-777777777777"));
        }
    }
}
