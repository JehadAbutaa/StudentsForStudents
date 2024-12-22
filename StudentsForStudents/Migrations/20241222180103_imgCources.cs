using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class imgCources : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Courses");

            migrationBuilder.AddColumn<byte[]>(
                name: "CourcePicture",
                table: "Courses",
                type: "longblob",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseRequests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 22, 21, 1, 3, 260, DateTimeKind.Local).AddTicks(6535),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(4379));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 22, 21, 1, 3, 260, DateTimeKind.Local).AddTicks(4436),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(974));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourcePicture",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Courses",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseRequests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(4379),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 22, 21, 1, 3, 260, DateTimeKind.Local).AddTicks(6535));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 23, 45, 31, 999, DateTimeKind.Local).AddTicks(974),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 22, 21, 1, 3, 260, DateTimeKind.Local).AddTicks(4436));
        }
    }
}
