using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class First_CRM_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "crm");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Telephone = table.Column<string>(type: "text", nullable: false),
                    NumberOfEmployees = table.Column<int>(type: "integer", nullable: false),
                    Site = table.Column<string>(type: "text", nullable: false),
                    OHServicesRequired = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Industry = table.Column<string>(type: "text", nullable: false),
                    Postcode = table.Column<string>(type: "text", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    InvoiceEmail = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    KeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DefaultPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    ChargeCode = table.Column<string>(type: "text", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Reference = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    RepresentativeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "crm",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerDocuments",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    BlobContainerName = table.Column<string>(type: "text", nullable: false),
                    BlobName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    SearchTags = table.Column<string>(type: "text", nullable: false),
                    DocumentGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerDocumentType = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CustomerDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerDocuments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "crm",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                schema: "crm",
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
                    Department = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Managers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Managers_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "crm",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ProductTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "crm",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalSchema: "crm",
                        principalTable: "ProductTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "crm",
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
                    DOB = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address1 = table.Column<string>(type: "text", nullable: false),
                    Address2 = table.Column<string>(type: "text", nullable: true),
                    Address3 = table.Column<string>(type: "text", nullable: true),
                    Postcode = table.Column<string>(type: "text", nullable: false),
                    JobRole = table.Column<string>(type: "text", nullable: false),
                    Department = table.Column<string>(type: "text", nullable: false),
                    LineManager = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SearchTags = table.Column<string>(type: "text", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "crm",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Managers_ManagerId",
                        column: x => x.ManagerId,
                        principalSchema: "crm",
                        principalTable: "Managers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocuments",
                schema: "crm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    BlobContainerName = table.Column<string>(type: "text", nullable: false),
                    BlobName = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    SearchTags = table.Column<string>(type: "text", nullable: false),
                    DocumentGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeDocumentType = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_EmployeeDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDocuments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "crm",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "crm",
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedBy", "CreatedByKeycloakId", "CreatedDate", "CustomerId", "Email", "Industry", "InvoiceEmail", "IsActive", "KeycloakId", "LastModifiedBy", "ModifiedByKeycloakId", "ModifiedDate", "Name", "Notes", "NumberOfEmployees", "OHServicesRequired", "Postcode", "SearchTags", "Site", "SubjectId", "SubjectKeycloakId", "Telephone", "TenantId", "TenantKeycloakId", "Website" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "1 Acme Street, London", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "info@acme.example.com", "Technology", "accounts@acme.example.com", true, new Guid("11111111-1111-1111-1111-111111111111"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Acme Corp", "Key client.", 100, "Full OH Service", "AC1 2ME", "1 Acme Corp 1 Acme Street London AC1 2ME 01234 567890", "London", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "01234 567890", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "https://acme.example.com" },
                    { new Guid("20202020-2020-2020-2020-202020202020"), "First Floor, Swan Buildings, 20 Swan Street, Manchester", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "contact@nationoh.co.uk", "Occupatioanl Health", "contact@nationoh.co.uk", true, new Guid("44444444-4444-4444-4444-444444444444"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Nation Occupational Health", "", 200, "Ad hoc assessments", "M4 5JW", "20 Nation Occupational Health First Floor Swan Buildings 20 Swan Street Manchester M4 5JW 01147 004 362", "Birmingham", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "01147 004 362", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "https://www.nationoh.co.uk" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "2 Beta Road, Manchester", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "contact@beta.example.com", "Manufacturing", "finance@beta.example.com", true, new Guid("22222222-2222-2222-2222-222222222222"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Beta Ltd", "Annual contract.", 50, "Health Surveillance", "BT2 3LT", "2 Beta Ltd 2 Beta Road Manchester BT2 3LT 02345 678901", "Manchester", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "02345 678901", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "https://beta.example.com" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "3 Gamma Avenue, Birmingham", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "hello@gamma.example.com", "Logistics", "billing@gamma.example.com", true, new Guid("33333333-3333-3333-3333-333333333333"), "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "Gamma Inc", "Occasional work.", 200, "Ad hoc assessments", "GM3 4IN", "", "Birmingham", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "03456 789012", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "https://gamma.example.com" }
                });

            migrationBuilder.InsertData(
                schema: "crm",
                table: "ProductTypes",
                columns: new[] { "Id", "ChargeCode", "CreatedBy", "CreatedByKeycloakId", "CreatedDate", "DefaultPrice", "Description", "EndTime", "IsActive", "LastModifiedBy", "ModifiedByKeycloakId", "ModifiedDate", "Name", "StartTime", "SubjectId", "SubjectKeycloakId", "TenantId", "TenantKeycloakId" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), "OHPTIME", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Consultancy Time (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Consultancy Time (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), "OHPFULL", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Full Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Full Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111112"), "PPHA", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Pre Placement Health Assessment", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pre Placement Health Assessment", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111113"), "PPHA15", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Pre Placement Health Assessment (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pre Placement Health Assessment (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111114"), "MTRC", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Mileage and Travel Re-Charged to Customer (per mile)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mileage and Travel Re-Charged to Customer (per mile)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111115"), "ARC", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Accommodation Recharged to Customer", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Accommodation Recharged to Customer", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111116"), "CRC", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Consumables Recharged to Customer", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Consumables Recharged to Customer", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111117"), "PSRC", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Physiotherapy Services Recharged to Customer", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Physiotherapy Services Recharged to Customer", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111118"), "GPSR", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "GP / Specialist Report Recharged", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "GP / Specialist Report Recharged", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111119"), "ADMIN", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Administration Time", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Administration Time", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111120"), "HAVS1", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 1", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 1", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111121"), "HAVS2", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 2", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 2", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111122"), "HAVS3", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 3", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 3", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111123"), "HAVS4", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "HAVS Tier 4", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HAVS Tier 4", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111124"), "OHPFULLCOMPLEX", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Full Complex", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Full Complex", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111125"), "PTSMINI", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work mini audit", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work mini audit", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111126"), "PTSPAPER", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work paper based review", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work paper based review", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111127"), "PTSAUDIT", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work audit of cases", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work audit of cases", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111128"), "PTSRETAIN", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "PTS / Rail Work Retainer (per month)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "PTS / Rail Work Retainer (per month)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("11111111-1111-1111-1111-111111111129"), "PTSADD", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Additional PTS or MRO work or reporting (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Additional PTS or MRO work or reporting (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "OHPHALF", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1000m, "OHP Half Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Half Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "OHAFULL", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHA Full Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHA Full Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "OHAHALF", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHA Half Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHA Half Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "OHTFULLDAY", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHT Full Day", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHT Full Day", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "OHPAPP", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "OHP Appointment", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "OHP Appointment", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "OHPPENS", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Pensions Case", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pensions Case", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "OHPAUDIO", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Audiometry Reviews (per 15 mins)", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Audiometry Reviews (per 15 mins)", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "RETAIN", "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 85m, "Monthly Retainer Fee", new DateTime(2026, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), false, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monthly Retainer Fee", new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.InsertData(
                schema: "crm",
                table: "Employees",
                columns: new[] { "Id", "Address1", "Address2", "Address3", "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedBy", "CreatedByKeycloakId", "CreatedDate", "CustomerId", "DOB", "Department", "Email", "FirstName", "IsActive", "JobRole", "KeycloakId", "LastModifiedBy", "LastName", "LineManager", "ManagerId", "ModifiedByKeycloakId", "ModifiedDate", "Notes", "Postcode", "SearchTags", "SubjectId", "SubjectKeycloakId", "Telephone", "TenantId", "TenantKeycloakId", "UserName" },
                values: new object[,]
                {
                    { new Guid("10101010-dddd-dddd-dddd-101010101010"), "10 Main St", "Apt 10", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(1989, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Facilities", "jack.white@example.com", "Jack", true, "Cleaner", new Guid("44444444-dddd-dddd-dddd-444444444444"), "System", "White", "Grace Brown", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP10 0JJ", "10 Jack White 10 Main St Apt 10 EMP10 0JJ 07000 000000", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07000 000000", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), "1 Main St", "Apt 1", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HR", "alice.smith@example.com", "Alice", true, "Manager", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "System", "Smith", "Bob Jones", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP1 1AA", "1 Alice Smith 1 Main St Apt 1 EMP1 1AA 07111 111111", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07111 111111", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), "2 Main St", "Apt 2", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1985, 2, 2, 0, 0, 0, 0, DateTimeKind.Utc), "IT", "bob.jones@example.com", "Bob", true, "Engineer", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "System", "Jones", "Carol White", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP2 2BB", "2 Bob Jones 2 Main St Apt 2 EMP2 2BB 07222 222222", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07222 222222", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("33333333-cccc-cccc-cccc-333333333333"), "3 Main St", "Apt 3", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1992, 3, 3, 0, 0, 0, 0, DateTimeKind.Utc), "Finance", "carol.white@example.com", "Carol", true, "Analyst", new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "System", "White", "David Black", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP3 3CC", "3 Carol White 3 Main St Apt 3 EMP3 3CC 07333 333333", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07333 333333", new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("44444444-dddd-dddd-dddd-444444444444"), "4 Main St", "Apt 4", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(1988, 4, 4, 0, 0, 0, 0, DateTimeKind.Utc), "Consulting", "david.black@example.com", "David", true, "Consultant", new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "System", "Black", "Alice Smith", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP4 4DD", "4 David Black 4 Main St Apt 4 EMP4 4DD 07444 444444", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07444 444444", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("55555555-eeee-eeee-eeee-555555555555"), "5 Main St", "Apt 5", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1995, 5, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Medical", "eve.green@example.com", "Eve", true, "Nurse", new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "System", "Green", "Bob Jones", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP5 5EE", "5 Eve Green 5 Main St Apt 5 EMP5 5EE 07555 555555", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07555 555555", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("66666666-ffff-ffff-ffff-666666666666"), "6 Main St", "Apt 6", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1983, 6, 6, 0, 0, 0, 0, DateTimeKind.Utc), "Support", "frank.blue@example.com", "Frank", true, "Technician", new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "System", "Blue", "Carol White", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP6 6FF", "6 Frank Blue 6 Main St Apt 6 EMP6 6FF 07666 666666", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07666 666666", new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("77777777-aaaa-aaaa-aaaa-777777777777"), "7 Main St", "Apt 7", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(1991, 7, 7, 0, 0, 0, 0, DateTimeKind.Utc), "Advisory", "grace.brown@example.com", "Grace", true, "Advisor", new Guid("11111111-aaaa-aaaa-aaaa-111111111111"), "System", "Brown", "David Black", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP7 7GG", "7 Grace Brown 7 Main St Apt 7 EMP7 7GG 07777 777777", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07777 777777", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("88888888-bbbb-bbbb-bbbb-888888888888"), "8 Main St", "Apt 8", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(1987, 8, 8, 0, 0, 0, 0, DateTimeKind.Utc), "Logistics", "henry.gray@example.com", "Henry", true, "Driver", new Guid("22222222-bbbb-bbbb-bbbb-222222222222"), "System", "Gray", "Eve Green", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP8 8HH", "8 Henry Gray 8 Main St Apt 8 EMP8 8HH 07888 888888", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07888 888888", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("99999999-cccc-cccc-cccc-999999999999"), "9 Main St", "Apt 9", "", null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(1993, 9, 9, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", "ivy.violet@example.com", "Ivy", true, "Receptionist", new Guid("33333333-cccc-cccc-cccc-333333333333"), "System", "Violet", "Frank Blue", null, new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(2025, 4, 16, 17, 0, 0, 0, DateTimeKind.Utc), "", "EMP9 9II", "9 Ivy Violet 9 Main St Apt 9 EMP9 9II 07999 999999", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07999 999999", new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.InsertData(
                schema: "crm",
                table: "Managers",
                columns: new[] { "Id", "AvatarDescription", "AvatarImage", "AvatarTitle", "CreatedBy", "CreatedByKeycloakId", "CreatedDate", "CustomerId", "Department", "Email", "FirstName", "IsActive", "KeycloakId", "LastModifiedBy", "LastName", "ModifiedByKeycloakId", "ModifiedDate", "SearchTags", "SubjectId", "SubjectKeycloakId", "Telephone", "TenantId", "TenantKeycloakId", "UserName" },
                values: new object[,]
                {
                    { new Guid("11111111-5555-5555-5555-111111111111"), null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("11111111-1111-1111-1111-111111111111"), "HR", "thompson.smith@example.com", "Thompson", false, new Guid("55555555-5555-5555-5555-555555555555"), "System", "Smith", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "1 Thompson Smith 1 Acme Street London AC1 2ME 07111 111111", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07111 111111", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("22222222-6666-6666-6666-222222222222"), null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("22222222-2222-2222-2222-222222222222"), "Finance", "emily.johnson@example.com", "Emily", false, new Guid("66666666-6666-6666-6666-666666666666"), "System", "Johnson", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "2 Emily Johnson 2 Beta Road Manchester BT2 3LT 07222 222222", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07222 222222", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("00000000-0000-0000-0000-000000000000"), null },
                    { new Guid("33333333-7777-7777-7777-333333333333"), null, null, null, "System", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-3333-3333-3333-333333333333"), "IT", "michael.brown@example.com", "Michael", false, new Guid("77777777-7777-7777-7777-777777777777"), "System", "Brown", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3 Michael Brown 3 Gamma Avenue Birmingham GM3 4IN 07333 333333", new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000"), "07333 333333", new Guid("33333333-3333-3333-3333-333333333333"), new Guid("00000000-0000-0000-0000-000000000000"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CustomerId",
                schema: "crm",
                table: "Contracts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerDocuments_CustomerId",
                schema: "crm",
                table: "CustomerDocuments",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_EmployeeId",
                schema: "crm",
                table: "EmployeeDocuments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CustomerId",
                schema: "crm",
                table: "Employees",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                schema: "crm",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_CustomerId",
                schema: "crm",
                table: "Managers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerId",
                schema: "crm",
                table: "Products",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                schema: "crm",
                table: "Products",
                column: "ProductTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contracts",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "CustomerDocuments",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "EmployeeDocuments",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "ProductTypes",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Managers",
                schema: "crm");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "crm");
        }
    }
}
