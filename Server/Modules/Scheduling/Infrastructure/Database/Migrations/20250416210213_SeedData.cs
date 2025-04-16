using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "scheduling",
                table: "Clinicians",
                columns: new[] { "Id", "ClinicianType", "CreatedBy", "CreatedDate", "Email", "FirstName", "IsActive", "LastModifiedBy", "LastName", "LicenceNumber", "ModifiedDate", "ProfilePictureUrl", "RegulatorType", "Telephone" },
                values: new object[,]
                {
                    { 1L, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.smith@example.com", "Alice", false, 0, "Smith", "GMC1001", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/women/1.jpg", 1, "555-1001" },
                    { 2L, 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.johnson@example.com", "Bob", false, 0, "Johnson", "GMC1002", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/men/2.jpg", 1, "555-1002" },
                    { 3L, 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "carol.williams@example.com", "Carol", false, 0, "Williams", "NMC1003", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/women/3.jpg", 2, "555-1003" },
                    { 4L, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.brown@example.com", "David", false, 0, "Brown", "GMC1004", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/men/4.jpg", 1, "555-1004" },
                    { 5L, 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eva.jones@example.com", "Eva", false, 0, "Jones", "NMC1005", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/women/5.jpg", 2, "555-1005" },
                    { 6L, 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.garcia@example.com", "Frank", false, 0, "Garcia", "GMC1006", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/men/6.jpg", 1, "555-1006" },
                    { 7L, 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.martinez@example.com", "Grace", false, 0, "Martinez", "NMC1007", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/women/7.jpg", 2, "555-1007" },
                    { 8L, 1, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "henry.lee@example.com", "Henry", false, 0, "Lee", "GMC1008", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/men/8.jpg", 1, "555-1008" },
                    { 9L, 3, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivy.walker@example.com", "Ivy", false, 0, "Walker", "NMC1009", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/women/9.jpg", 2, "555-1009" },
                    { 10L, 2, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jack.hall@example.com", "Jack", false, 0, "Hall", "GMC1010", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "https://randomuser.me/api/portraits/men/10.jpg", 1, "555-1010" }
                });

            migrationBuilder.InsertData(
                schema: "scheduling",
                table: "Referrals",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CustomerId", "DocumentId", "IsActive", "LastModifiedBy", "ModifiedDate", "PatientId", "ReferralDetails" },
                values: new object[,]
                {
                    { 1L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "DOC-1001", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "Routine checkup for hypertension." },
                    { 2L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "DOC-1002", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "Follow-up for diabetes management." },
                    { 3L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L, "DOC-1003", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "Initial consultation for back pain." },
                    { 4L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "DOC-1004", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L, "Annual physical examination." },
                    { 5L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "DOC-1005", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L, "Referral for allergy testing." },
                    { 6L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L, "DOC-1006", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6L, "Consultation for asthma symptoms." },
                    { 7L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "DOC-1007", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7L, "Pre-surgery evaluation." },
                    { 8L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "DOC-1008", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8L, "Post-operative follow-up." },
                    { 9L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "DOC-1009", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9L, "Referral for physical therapy." },
                    { 10L, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L, "DOC-1010", false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10L, "Consultation for migraine headaches." }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L);
        }
    }
}
