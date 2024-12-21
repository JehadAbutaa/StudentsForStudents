using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class editevent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Events");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseRequests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(4379),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 23, 17, 17, 967, DateTimeKind.Local).AddTicks(6338));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(974),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 23, 17, 17, 967, DateTimeKind.Local).AddTicks(3079));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Events",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseRequests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 23, 17, 17, 967, DateTimeKind.Local).AddTicks(6338),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(4379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 23, 17, 17, 967, DateTimeKind.Local).AddTicks(3079),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(974));
        }
    }
}
