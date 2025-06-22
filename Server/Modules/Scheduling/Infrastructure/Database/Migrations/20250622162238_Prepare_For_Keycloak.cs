using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Prepare_For_Keycloak : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "scheduling",
                table: "Clinicians",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "JackHall" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "AliceSmith" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "BobJohnson" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "CarolWilliams" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "DavidBrown" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "EvaJones" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "FrankGarcia" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "GraceMartinez" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "HenryLee" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "UserName", "Username" },
                values: new object[] { null, "IvyWalker" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.RenameColumn(
                name: "Username",
                schema: "scheduling",
                table: "Clinicians",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "UserName",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "UserName",
                value: null);
        }
    }
}
