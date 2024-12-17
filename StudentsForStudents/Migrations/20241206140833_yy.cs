using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class yy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Teacher",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "Rate",
                table: "Teacher",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 6, 17, 8, 33, 516, DateTimeKind.Local).AddTicks(4058),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 6, 15, 32, 13, 135, DateTimeKind.Local).AddTicks(8826));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Teacher");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 6, 15, 32, 13, 135, DateTimeKind.Local).AddTicks(8826),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 6, 17, 8, 33, 516, DateTimeKind.Local).AddTicks(4058));
        }
    }
}
