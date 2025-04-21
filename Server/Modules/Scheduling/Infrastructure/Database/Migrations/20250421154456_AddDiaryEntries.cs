using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDiaryEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "scheduling",
                table: "Schedules",
                columns: new[] { "Id", "ClinicianId", "CreatedBy", "CreatedDate", "CustomerId", "End", "IsActive", "LastModifiedBy", "ModifiedDate", "PatientId", "ReferralId", "Start" },
                values: new object[,]
                {
                    { 1L, 1L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, new DateTime(2025, 4, 22, 10, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, 1L, new DateTime(2025, 4, 22, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, 2L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, new DateTime(2025, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, 2L, new DateTime(2025, 4, 22, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, 3L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, new DateTime(2025, 4, 22, 12, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, 3L, new DateTime(2025, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L);
        }
    }
}
