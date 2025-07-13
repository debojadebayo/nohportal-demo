using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Shared.DTOs.Scheduling;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Referral_Details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ReferralDetailsDto>(
                name: "Details",
                schema: "scheduling",
                table: "Referrals",
                type: "jsonb",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "Details",
                value: null);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "Details",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                schema: "scheduling",
                table: "Referrals");
        }
    }
}
