using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSearchTagsToAllRelevantEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchTags",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                column: "SearchTags",
                value: "1 Alice Smith, Senior Doctor, GMC, GMC1001");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                column: "SearchTags",
                value: "2 Bob Johnson, Junior Doctor, GMC, GMC1002");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                column: "SearchTags",
                value: "3 Carol Williams, Nurse, NMC, NMC1003");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                column: "SearchTags",
                value: "4 David Brown, Senior Doctor, GMC, GMC1004");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                column: "SearchTags",
                value: "5 Eva Jones, Nurse, NMC, NMC1005");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                column: "SearchTags",
                value: "6 Frank Garcia, Junior Doctor, GMC, GMC1006");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                column: "SearchTags",
                value: "7 Grace Martinez, Nurse, NMC, NMC1007");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                column: "SearchTags",
                value: "8 Henry Lee, Senior Doctor, GMC, GMC1008");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                column: "SearchTags",
                value: "9 Ivy Walker, Nurse, NMC, NMC1009");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                column: "SearchTags",
                value: "10 Jack Hall, Junior Doctor, GMC, GMC1010");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchTags",
                schema: "scheduling",
                table: "Clinicians");
        }
    }
}
