using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Modules.Scheduling.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreateCalendarInterfaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                schema: "scheduling",
                table: "Schedules",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                schema: "scheduling",
                table: "Schedules",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "ProfilePictureUrl",
                schema: "scheduling",
                table: "Clinicians",
                newName: "AvatarTitle");

            migrationBuilder.AddColumn<string>(
                name: "AvatarDescription",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AvatarImage",
                schema: "scheduling",
                table: "Clinicians",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Senior Doctor, GMC", "https://randomuser.me/api/portraits/women/1.jpg", "Dr. Alice Smith" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Junior Doctor, GMC", "https://randomuser.me/api/portraits/men/2.jpg", "Dr. Bob Johnson" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Nurse, NMC", "https://randomuser.me/api/portraits/women/3.jpg", "Nurse Carol Williams" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Senior Doctor, GMC", "https://randomuser.me/api/portraits/men/4.jpg", "Dr. David Brown" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Nurse, NMC", "https://randomuser.me/api/portraits/women/5.jpg", "Nurse Eva Jones" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Junior Doctor, GMC", "https://randomuser.me/api/portraits/men/6.jpg", "Dr. Frank Garcia" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Nurse, NMC", "https://randomuser.me/api/portraits/women/7.jpg", "Nurse Grace Martinez" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Senior Doctor, GMC", "https://randomuser.me/api/portraits/men/8.jpg", "Dr. Henry Lee" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Nurse, NMC", "https://randomuser.me/api/portraits/women/9.jpg", "Nurse Ivy Walker" });

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "AvatarDescription", "AvatarImage", "AvatarTitle" },
                values: new object[] { "Junior Doctor, GMC", "https://randomuser.me/api/portraits/men/10.jpg", "Dr. Jack Hall" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarDescription",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.DropColumn(
                name: "AvatarImage",
                schema: "scheduling",
                table: "Clinicians");

            migrationBuilder.RenameColumn(
                name: "Start",
                schema: "scheduling",
                table: "Schedules",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "End",
                schema: "scheduling",
                table: "Schedules",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "AvatarTitle",
                schema: "scheduling",
                table: "Clinicians",
                newName: "ProfilePictureUrl");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 1L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/women/1.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 2L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/men/2.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 3L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/women/3.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 4L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/men/4.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 5L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/women/5.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 6L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/men/6.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 7L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/women/7.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 8L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/men/8.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 9L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/women/9.jpg");

            migrationBuilder.UpdateData(
                schema: "scheduling",
                table: "Clinicians",
                keyColumn: "Id",
                keyValue: 10L,
                column: "ProfilePictureUrl",
                value: "https://randomuser.me/api/portraits/men/10.jpg");
        }
    }
}
