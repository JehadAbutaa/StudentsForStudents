using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class Tes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Teacher",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "StudentId",
                table: "Students",
                type: "varchar(12)",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Students",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Teacher",
                type: "int",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Students",
                type: "int",
                maxLength: 12,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12);

            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Students",
                type: "int",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);
        }
    }
}
