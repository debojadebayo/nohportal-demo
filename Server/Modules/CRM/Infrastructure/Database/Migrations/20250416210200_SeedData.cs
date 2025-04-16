using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "crm",
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedDate", "Email", "Industry", "InvoiceEmail", "IsActive", "LastModifiedBy", "ModifiedDate", "Name", "Notes", "NumberOfEmployees", "OHServicesRequired", "Postcode", "Site", "Telephone", "Website" },
                values: new object[,]
                {
                    { 1L, "1 Acme Street, London", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "info@acme.example.com", "Technology", "accounts@acme.example.com", true, 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Acme Corp", "Key client.", 100, "Full OH Service", "AC1 2ME", "London", "01234 567890", "https://acme.example.com" },
                    { 2L, "2 Beta Road, Manchester", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "contact@beta.example.com", "Manufacturing", "finance@beta.example.com", true, 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Beta Ltd", "Annual contract.", 50, "Health Surveillance", "BT2 3LT", "Manchester", "02345 678901", "https://beta.example.com" },
                    { 3L, "3 Gamma Avenue, Birmingham", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "hello@gamma.example.com", "Logistics", "billing@gamma.example.com", true, 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Gamma Inc", "Occasional work.", 200, "Ad hoc assessments", "GM3 4IN", "Birmingham", "03456 789012", "https://gamma.example.com" }
                });

            migrationBuilder.InsertData(
                schema: "crm",
                table: "Employees",
                columns: new[] { "Id", "Address1", "Address2", "Address3", "CompanyName", "CreatedBy", "CreatedDate", "DOB", "Department", "Email", "FirstName", "IsActive", "JobRole", "LastModifiedBy", "LastName", "LineManager", "ModifiedDate", "Postcode", "Telephone" },
                values: new object[,]
                {
                    { 1L, "1 Main St", "Apt 1", "", "Acme Corp", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HR", "alice.smith@example.com", "Alice", true, "Manager", 1, "Smith", "Bob Jones", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP1 1AA", "07111 111111" },
                    { 2L, "2 Main St", "Apt 2", "", "Beta Ltd", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1985, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "IT", "bob.jones@example.com", "Bob", true, "Engineer", 1, "Jones", "Carol White", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP2 2BB", "07222 222222" },
                    { 3L, "3 Main St", "Apt 3", "", "Gamma Inc", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1992, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Finance", "carol.white@example.com", "Carol", true, "Analyst", 1, "White", "David Black", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP3 3CC", "07333 333333" },
                    { 4L, "4 Main St", "Apt 4", "", "Acme Corp", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1988, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Consulting", "david.black@example.com", "David", true, "Consultant", 1, "Black", "Alice Smith", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP4 4DD", "07444 444444" },
                    { 5L, "5 Main St", "Apt 5", "", "Beta Ltd", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Medical", "eve.green@example.com", "Eve", true, "Nurse", 1, "Green", "Bob Jones", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP5 5EE", "07555 555555" },
                    { 6L, "6 Main St", "Apt 6", "", "Gamma Inc", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1983, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Support", "frank.blue@example.com", "Frank", true, "Technician", 1, "Blue", "Carol White", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP6 6FF", "07666 666666" },
                    { 7L, "7 Main St", "Apt 7", "", "Acme Corp", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1991, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Advisory", "grace.brown@example.com", "Grace", true, "Advisor", 1, "Brown", "David Black", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP7 7GG", "07777 777777" },
                    { 8L, "8 Main St", "Apt 8", "", "Beta Ltd", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1987, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Logistics", "henry.gray@example.com", "Henry", true, "Driver", 1, "Gray", "Eve Green", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP8 8HH", "07888 888888" },
                    { 9L, "9 Main St", "Apt 9", "", "Gamma Inc", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1993, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", "ivy.violet@example.com", "Ivy", true, "Receptionist", 1, "Violet", "Frank Blue", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP9 9II", "07999 999999" },
                    { 10L, "10 Main St", "Apt 10", "", "Acme Corp", 1, new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new DateTime(1989, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Facilities", "jack.white@example.com", "Jack", true, "Cleaner", 1, "White", "Grace Brown", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "EMP10 0JJ", "07000 000000" }
                });

            migrationBuilder.InsertData(
                schema: "crm",
                table: "ProductTypes",
                columns: new[] { "Id", "ChargeCode", "CreatedBy", "CreatedDate", "DefaultPrice", "Description", "EndTime", "IsActive", "LastModifiedBy", "ModifiedDate", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1L, "OHPFULL", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Full Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Full Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 2L, "OHPHALF", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000m, "OHP Half Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Half Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 3L, "OHAFULL", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHA Full Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHA Full Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 4L, "OHAHALF", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHA Half Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHA Half Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 5L, "OHTFULLDAY", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHT Full Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHT Full Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 6L, "OHPAPP", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Appointment", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Appointment", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 7L, "OHPPENS", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Pensions Case", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pensions Case", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 8L, "OHPAUDIO", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Audiometry Reviews (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Audiometry Reviews (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 9L, "RETAIN", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Monthly Retainer Fee", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monthly Retainer Fee", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 10L, "OHPTIME", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Consultancy Time (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Consultancy Time (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 11L, "PPHA", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Pre Placement Health Assessment", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pre Placement Health Assessment", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 12L, "PPHA15", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Pre Placement Health Assessment (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pre Placement Health Assessment (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 13L, "MTRC", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Mileage and Travel Re-Charged to Customer (per mile)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mileage and Travel Re-Charged to Customer (per mile)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 14L, "ARC", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Accommodation Recharged to Customer", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accommodation Recharged to Customer", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 15L, "CRC", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Consumables Recharged to Customer", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consumables Recharged to Customer", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 16L, "PSRC", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Physiotherapy Services Recharged to Customer", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Physiotherapy Services Recharged to Customer", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 17L, "GPSR", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "GP / Specialist Report Recharged", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GP / Specialist Report Recharged", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 18L, "ADMIN", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Administration Time", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administration Time", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 19L, "HAVS1", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 1", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 1", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 20L, "HAVS2", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 2", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 2", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 21L, "HAVS3", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 3", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 3", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 22L, "HAVS4", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 4", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 4", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 23L, "OHPFULLCOMPLEX", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Full Complex", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Full Complex", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 24L, "PTSMINI", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work mini audit", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work mini audit", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 25L, "PTSPAPER", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work paper based review", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work paper based review", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 26L, "PTSAUDIT", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work audit of cases", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work audit of cases", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 27L, "PTSRETAIN", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work Retainer (per month)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work Retainer (per month)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 28L, "PTSADD", 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Additional PTS or MRO work or reporting (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Additional PTS or MRO work or reporting (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 28L);
        }
    }
}
