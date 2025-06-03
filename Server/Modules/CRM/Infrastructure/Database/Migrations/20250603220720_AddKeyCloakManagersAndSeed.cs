using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddKeyCloakManagersAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "KeycloakId",
                schema: "crm",
                table: "Managers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                schema: "crm",
                table: "Managers",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CustomerId", "Department", "Email", "IsActive", "KeycloakId", "LastModifiedBy", "ModifiedDate", "Name", "Phone", "SubjectId", "TenantId" },
                values: new object[,]
                {
                    { 1L, 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "HR", "thompson.smith@example.com", false, new Guid("55555555-5555-5555-5555-555555555555"), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thompson Smith", "07111 111111", 0L, 1L },
                    { 2L, 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "Finance", "emily.johnson@example.com", false, new Guid("66666666-6666-6666-6666-666666666666"), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emily Johnson", "07222 222222", 0L, 2L },
                    { 3L, 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "IT", "michael.brown@example.com", false, new Guid("77777777-7777-7777-7777-777777777777"), 0L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Michael Brown", "07333 333333", 0L, 3L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Managers",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "KeycloakId",
                schema: "crm",
                table: "Managers");
        }
    }
}
