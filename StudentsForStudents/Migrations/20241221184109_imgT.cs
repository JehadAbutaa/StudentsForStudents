using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class imgT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Teacher");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "Teacher",
                type: "longblob",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseRequests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 21, 41, 9, 746, DateTimeKind.Local).AddTicks(3011),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 18, 42, 38, 994, DateTimeKind.Local).AddTicks(5784));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 21, 41, 9, 746, DateTimeKind.Local).AddTicks(1127),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 18, 42, 38, 994, DateTimeKind.Local).AddTicks(3841));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Teacher");

            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Teacher",
                type: "longtext",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CourseRequests",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 18, 42, 38, 994, DateTimeKind.Local).AddTicks(5784),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 21, 41, 9, 746, DateTimeKind.Local).AddTicks(3011));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 21, 18, 42, 38, 994, DateTimeKind.Local).AddTicks(3841),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 21, 21, 41, 9, 746, DateTimeKind.Local).AddTicks(1127));
        }
    }
}
