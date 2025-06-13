using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComposedHealthBase.Server.Auth.AuthDatabase.Migrations
{
    /// <inheritdoc />
    public partial class MigrateBrokenPermissionRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionRole",
                schema: "auth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantKeycloakMap",
                schema: "auth",
                table: "TenantKeycloakMap");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectKeycloakMap",
                schema: "auth",
                table: "SubjectKeycloakMap");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "auth",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "auth",
                table: "Permission");

            migrationBuilder.RenameTable(
                name: "TenantKeycloakMap",
                schema: "auth",
                newName: "TenantKeycloakMaps",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "SubjectKeycloakMap",
                schema: "auth",
                newName: "SubjectKeycloakMaps",
                newSchema: "auth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantKeycloakMaps",
                schema: "auth",
                table: "TenantKeycloakMaps",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectKeycloakMaps",
                schema: "auth",
                table: "SubjectKeycloakMaps",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "auth",
                columns: table => new
                {
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PermissionId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "auth",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Role",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Administrator" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                schema: "auth",
                table: "RolePermission",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "auth");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantKeycloakMaps",
                schema: "auth",
                table: "TenantKeycloakMaps");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubjectKeycloakMaps",
                schema: "auth",
                table: "SubjectKeycloakMaps");

            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameTable(
                name: "TenantKeycloakMaps",
                schema: "auth",
                newName: "TenantKeycloakMap",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "SubjectKeycloakMaps",
                schema: "auth",
                newName: "SubjectKeycloakMap",
                newSchema: "auth");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "auth",
                table: "Role",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "auth",
                table: "Permission",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantKeycloakMap",
                schema: "auth",
                table: "TenantKeycloakMap",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubjectKeycloakMap",
                schema: "auth",
                table: "SubjectKeycloakMap",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PermissionRole",
                schema: "auth",
                columns: table => new
                {
                    PermissionsId = table.Column<long>(type: "bigint", nullable: false),
                    RolesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionRole", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_PermissionRole_Permission_PermissionsId",
                        column: x => x.PermissionsId,
                        principalSchema: "auth",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionRole_Role_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "auth",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionRole_RolesId",
                schema: "auth",
                table: "PermissionRole",
                column: "RolesId");
        }
    }
}
