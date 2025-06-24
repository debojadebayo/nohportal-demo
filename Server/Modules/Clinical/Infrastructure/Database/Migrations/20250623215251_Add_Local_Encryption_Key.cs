using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Clinical.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Local_Encryption_Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocalEncryptionKey",
                schema: "clinical",
                table: "CaseNotes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalEncryptionKey",
                schema: "clinical",
                table: "CaseNotes");
        }
    }
}
