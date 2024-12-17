using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class tt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 16, 37, 783, DateTimeKind.Local).AddTicks(2706),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 3, 19, 51, 47, 455, DateTimeKind.Local).AddTicks(8096));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 19, 51, 47, 455, DateTimeKind.Local).AddTicks(8096),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 16, 37, 783, DateTimeKind.Local).AddTicks(2706));
        }
    }
}
