using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class q : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Students",
                type: "longtext",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 7, 20, 13, 47, 641, DateTimeKind.Local).AddTicks(5948),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 6, 17, 8, 33, 516, DateTimeKind.Local).AddTicks(4058));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Students");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 6, 17, 8, 33, 516, DateTimeKind.Local).AddTicks(4058),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 7, 20, 13, 47, 641, DateTimeKind.Local).AddTicks(5948));
        }
    }
}
