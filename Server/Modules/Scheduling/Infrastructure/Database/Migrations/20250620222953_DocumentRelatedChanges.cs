using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class DocumentRelatedChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                schema: "scheduling",
                name: "RelatedDocumentIds",
                table: "Referrals");

            migrationBuilder.AddColumn<Guid[]>(
                schema: "scheduling",
                name: "RelatedDocumentIds",
                table: "Referrals",
                type: "uuid[]",
                nullable: false,
                defaultValue: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                column: "RelatedDocumentIds",
                value: new Guid[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                schema: "scheduling",
                name: "RelatedDocumentIds",
                table: "Referrals");

            migrationBuilder.AddColumn<long[]>(
                schema: "scheduling",
                name: "RelatedDocumentIds",
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
    }
}
