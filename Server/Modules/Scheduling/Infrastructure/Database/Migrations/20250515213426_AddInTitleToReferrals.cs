using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddInTitleToReferrals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                schema: "scheduling",
                table: "Referrals",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Title",
                value: "Hypertension Checkup");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Title",
                value: "Diabetes Follow-up");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Title",
                value: "Back Pain Consultation");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Title",
                value: "Annual Physical Exam");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Title",
                value: "Allergy Testing");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Title",
                value: "Asthma Consultation");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Title",
                value: "Pre-Surgery Evaluation");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Title",
                value: "Post-Op Follow-up");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Title",
                value: "Physical Therapy Referral");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Title",
                value: "Migraine Consultation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                schema: "scheduling",
                table: "Referrals");
        }
    }
}
