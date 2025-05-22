using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.CRM.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddTenantSubjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "crm",
                table: "ProductTypes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "crm",
                table: "Products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "crm",
                table: "Managers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "crm",
                table: "Employees",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Documents",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Documents",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "BlobContainerName",
                schema: "crm",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BlobName",
                schema: "crm",
                table: "Documents",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "crm",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "crm",
                table: "Documents",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Customers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Customers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "crm",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "crm",
                table: "Customers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<long>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "CreatedBy",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TenantId",
                schema: "crm",
                table: "Contracts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 1L, 1L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedBy", "LastModifiedBy", "SubjectId", "TenantId" },
                values: new object[] { 0L, 0L, 0L, 0L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "crm",
                table: "ProductTypes");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "crm",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "crm",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "crm",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BlobContainerName",
                schema: "crm",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "BlobName",
                schema: "crm",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "crm",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "crm",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "crm",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "crm",
                table: "Contracts");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "ProductTypes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Managers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Managers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Employees",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Documents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Documents",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Customers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Customers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "LastModifiedBy",
                schema: "crm",
                table: "Contracts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "crm",
                table: "Contracts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 1, 1 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                schema: "crm",
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "CreatedBy", "LastModifiedBy" },
                values: new object[] { 0, 0 });
        }
    }
}
