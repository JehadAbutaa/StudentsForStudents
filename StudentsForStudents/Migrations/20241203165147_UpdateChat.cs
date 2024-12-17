using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SenderID",
                table: "Chats",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                table: "Chats",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 19, 51, 47, 455, DateTimeKind.Local).AddTicks(8096),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 3, 19, 46, 49, 242, DateTimeKind.Local).AddTicks(8315));

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "Chats",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "SenderName",
                table: "Chats",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "SenderName",
                table: "Chats");

            migrationBuilder.AlterColumn<int>(
                name: "SenderID",
                table: "Chats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiverId",
                table: "Chats",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 19, 46, 49, 242, DateTimeKind.Local).AddTicks(8315),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 3, 19, 51, 47, 455, DateTimeKind.Local).AddTicks(8096));
        }
    }
}
