using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class First_Scheduling_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "scheduling");

            migrationBuilder.CreateTable(
                name: "Clinicians",
                schema: "scheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    AvatarImage = table.Column<string>(type: "text", nullable: true),
                    AvatarTitle = table.Column<string>(type: "text", nullable: true),
                    AvatarDescription = table.Column<string>(type: "text", nullable: true),
                    KeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicianType = table.Column<int>(type: "integer", nullable: false),
                    RegulatorType = table.Column<int>(type: "integer", nullable: false),
                    LicenceNumber = table.Column<string>(type: "text", nullable: false),
                    SearchTags = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinicians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Referrals",
                schema: "scheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ReferralDetails = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ReferralStatus = table.Column<int>(type: "integer", nullable: false),
                    RelatedDocumentIds = table.Column<Guid[]>(type: "uuid[]", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referrals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                schema: "scheduling",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ReferralId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClinicianId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AppointmentStatus = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Clinicians_ClinicianId",
                        column: x => x.ClinicianId,
                        principalSchema: "scheduling",
                        principalTable: "Clinicians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Referrals_ReferralId",
                        column: x => x.ReferralId,
                        principalSchema: "scheduling",
                        principalTable: "Referrals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "scheduling",
                table: "Clinicians",
                columns: new[] { "Id", "AvatarDescription", "AvatarImage", "AvatarTitle", "ClinicianType", "CreatedBy", "CreatedByKeycloakId", "CreatedDate", "Email", "FirstName", "IsActive", "KeycloakId", "LastModifiedBy", "LastName", "LicenceNumber", "ModifiedByKeycloakId", "ModifiedDate", "RegulatorType", "SearchTags", "SubjectId", "SubjectKeycloakId", "Telephone", "TenantId", "TenantKeycloakId", "UserName" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), "Junior Doctor, GMC", "https://randomuser.me/api/portraits/men/10.jpg", "Dr. Jack Hall", 2, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jack.hall@example.com", "Jack", false, new Guid("a1b2c3d4-e5f6-a7b8-c9d0-e1f2a3b4c5d6"), "System", "Hall", "GMC1010", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "10 Jack Hall, Junior Doctor, GMC, GMC1010", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1010", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Senior Doctor, GMC", "https://randomuser.me/api/portraits/women/1.jpg", "Dr. Alice Smith", 1, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "alice.smith@example.com", "Alice", false, new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), "System", "Smith", "GMC1001", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "1 Alice Smith, Senior Doctor, GMC, GMC1001", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1001", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Junior Doctor, GMC", "https://randomuser.me/api/portraits/men/2.jpg", "Dr. Bob Johnson", 2, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "bob.johnson@example.com", "Bob", false, new Guid("9c8b7a6d-5e4f-3c2b-1a09-876543210fed"), "System", "Johnson", "GMC1002", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "2 Bob Johnson, Junior Doctor, GMC, GMC1002", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1002", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Nurse, NMC", "https://randomuser.me/api/portraits/women/3.jpg", "Nurse Carol Williams", 3, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "carol.williams@example.com", "Carol", false, new Guid("123e4567-e89b-12d3-a456-426614174000"), "System", "Williams", "NMC1003", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "3 Carol Williams, Nurse, NMC, NMC1003", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1003", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Senior Doctor, GMC", "https://randomuser.me/api/portraits/men/4.jpg", "Dr. David Brown", 1, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "david.brown@example.com", "David", false, new Guid("ba012345-6789-abcd-0123-456789abcdef"), "System", "Brown", "GMC1004", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "4 David Brown, Senior Doctor, GMC, GMC1004", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1004", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Nurse, NMC", "https://randomuser.me/api/portraits/women/5.jpg", "Nurse Eva Jones", 3, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "eva.jones@example.com", "Eva", false, new Guid("00112233-4455-6677-8899-aabbccddeeff"), "System", "Jones", "NMC1005", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "5 Eva Jones, Nurse, NMC, NMC1005", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1005", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Junior Doctor, GMC", "https://randomuser.me/api/portraits/men/6.jpg", "Dr. Frank Garcia", 2, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "frank.garcia@example.com", "Frank", false, new Guid("ffeeddcc-bbaa-9988-7766-554433221100"), "System", "Garcia", "GMC1006", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "6 Frank Garcia, Junior Doctor, GMC, GMC1006", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1006", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Nurse, NMC", "https://randomuser.me/api/portraits/women/7.jpg", "Nurse Grace Martinez", 3, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "grace.martinez@example.com", "Grace", false, new Guid("abcdef01-2345-6789-abcd-ef0123456789"), "System", "Martinez", "NMC1007", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "7 Grace Martinez, Nurse, NMC, NMC1007", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1007", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Senior Doctor, GMC", "https://randomuser.me/api/portraits/men/8.jpg", "Dr. Henry Lee", 1, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "henry.lee@example.com", "Henry", false, new Guid("fedcba98-7654-3210-fedc-ba9876543210"), "System", "Lee", "GMC1008", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "8 Henry Lee, Senior Doctor, GMC, GMC1008", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1008", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Nurse, NMC", "https://randomuser.me/api/portraits/women/9.jpg", "Nurse Ivy Walker", 3, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivy.walker@example.com", "Ivy", false, new Guid("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"), "System", "Walker", "NMC1009", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "9 Ivy Walker, Nurse, NMC, NMC1009", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "555-1009", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.InsertData(
                schema: "scheduling",
                table: "Referrals",
                columns: new[] { "Id", "CreatedBy", "CreatedByKeycloakId", "CreatedDate", "CustomerId", "EmployeeId", "IsActive", "LastModifiedBy", "ModifiedByKeycloakId", "ModifiedDate", "ReferralDetails", "ReferralStatus", "RelatedDocumentIds", "SubjectId", "SubjectKeycloakId", "TenantId", "TenantKeycloakId", "Title" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("10101010-dddd-dddd-dddd-101010101010"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consultation for migraine headaches.", 0, new Guid[0], new Guid("10101010-dddd-dddd-dddd-101010101010"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), "Migraine Consultation" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Routine checkup for hypertension.", 0, new Guid[0], new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), "Hypertension Checkup" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Follow-up for diabetes management.", 0, new Guid[0], new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), "Diabetes Follow-up" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("33333333-cccc-cccc-cccc-333333333333"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Initial consultation for back pain.", 0, new Guid[0], new Guid("33333333-cccc-cccc-cccc-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), "Back Pain Consultation" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("44444444-dddd-dddd-dddd-444444444444"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Annual physical examination.", 0, new Guid[0], new Guid("44444444-dddd-dddd-dddd-444444444444"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), "Annual Physical Exam" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("55555555-eeee-eeee-eeee-555555555555"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Referral for allergy testing.", 0, new Guid[0], new Guid("55555555-eeee-eeee-eeee-555555555555"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), "Allergy Testing" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("66666666-ffff-ffff-ffff-666666666666"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consultation for asthma symptoms.", 0, new Guid[0], new Guid("66666666-ffff-ffff-ffff-666666666666"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), "Asthma Consultation" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("77777777-aaaa-aaaa-aaaa-777777777777"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pre-surgery evaluation.", 0, new Guid[0], new Guid("77777777-aaaa-aaaa-aaaa-777777777777"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), "Pre-Surgery Evaluation" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("88888888-bbbb-bbbb-bbbb-888888888888"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Post-operative follow-up.", 0, new Guid[0], new Guid("88888888-bbbb-bbbb-bbbb-888888888888"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), "Post-Op Follow-up" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("99999999-cccc-cccc-cccc-999999999999"), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Referral for physical therapy.", 0, new Guid[0], new Guid("99999999-cccc-cccc-cccc-999999999999"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), "Physical Therapy Referral" }
                });

            migrationBuilder.InsertData(
                schema: "scheduling",
                table: "Schedules",
                columns: new[] { "Id", "AppointmentStatus", "ClinicianId", "CreatedBy", "CreatedByKeycloakId", "CreatedDate", "CustomerId", "Description", "EmployeeId", "End", "IsActive", "LastModifiedBy", "ModifiedByKeycloakId", "ModifiedDate", "ProductId", "ReferralId", "Start", "Status", "SubjectId", "SubjectKeycloakId", "TenantId", "TenantKeycloakId", "Title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 0, new Guid("11111111-1111-1111-1111-111111111111"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "Regular blood pressure monitoring and medication review", new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), new DateTime(2025, 4, 22, 10, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 4, 22, 9, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), "Blood Pressure Check-up" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 0, new Guid("22222222-2222-2222-2222-222222222222"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "Review of blood sugar levels and medication adjustment", new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), new DateTime(2025, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 4, 22, 10, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), "Diabetes Follow-up" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 0, new Guid("33333333-3333-3333-3333-333333333333"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "Initial evaluation of chronic lower back pain", new Guid("33333333-cccc-cccc-cccc-333333333333"), new DateTime(2025, 4, 22, 12, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc), 0, new Guid("33333333-cccc-cccc-cccc-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), "Back Pain Assessment" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ClinicianId",
                schema: "scheduling",
                table: "Schedules",
                column: "ClinicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ReferralId",
                schema: "scheduling",
                table: "Schedules",
                column: "ReferralId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schedules",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "Clinicians",
                schema: "scheduling");

            migrationBuilder.DropTable(
                name: "Referrals",
                schema: "scheduling");
        }
    }
}
