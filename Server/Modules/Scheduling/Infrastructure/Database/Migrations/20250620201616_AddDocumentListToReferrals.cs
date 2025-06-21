using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentListToReferrals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long[]>(
                name: "RelatedDocumentIds",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint[]",
                nullable: false,
                defaultValue: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                column: "RelatedDocumentIds",
                value: new long[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                column: "RelatedDocumentIds",
                value: new long[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RelatedDocumentIds",
                schema: "scheduling",
                table: "Referrals");
        }
    }
}
