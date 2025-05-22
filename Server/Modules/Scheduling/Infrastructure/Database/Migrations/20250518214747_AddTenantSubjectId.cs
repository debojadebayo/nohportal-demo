using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantSubjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "scheduling",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "scheduling",
                table: "Schedules",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "scheduling",
                table: "Referrals",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "scheduling",
                table: "Clinicians",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "scheduling",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "scheduling",
                table: "Referrals");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Schedules",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Referrals",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "scheduling",
                table: "Clinicians",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Referrals",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Schedules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });
        }
    }
}
