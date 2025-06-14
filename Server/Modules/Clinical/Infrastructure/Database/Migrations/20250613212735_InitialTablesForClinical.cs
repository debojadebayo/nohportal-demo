using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Server.Modules.Clinical.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialTablesForClinical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "clinical");

            migrationBuilder.CreateTable(
                name: "CaseNotes",
                schema: "clinical",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaseNotes = table.Column<string>(type: "text", nullable: true),
                    AppointmentType = table.Column<int>(type: "integer", nullable: false),
                    FitForWorkStatus = table.Column<int>(type: "integer", nullable: false),
                    RecommendedAdjustments = table.Column<string>(type: "text", nullable: true),
                    IsFollowUpNeeded = table.Column<bool>(type: "boolean", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FollowUpReasonForReferral = table.Column<string>(type: "text", nullable: true),
                    ClinicianId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    TenantKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseNotes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalReports",
                schema: "clinical",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmployeeId = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeName = table.Column<string>(type: "text", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Company = table.Column<string>(type: "text", nullable: true),
                    JobRole = table.Column<string>(type: "text", nullable: true),
                    ReportType = table.Column<int>(type: "integer", nullable: false),
                    AssessmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateReportSubmitted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReportNotes = table.Column<string>(type: "text", nullable: true),
                    ClinicianId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TenantId = table.Column<long>(type: "bigint", nullable: false),
                    SubjectId = table.Column<long>(type: "bigint", nullable: false),
                    TenantKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedByKeycloakId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalReports", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseNotes",
                schema: "clinical");

            migrationBuilder.DropTable(
                name: "ClinicalReports",
                schema: "clinical");
        }
    }
}
