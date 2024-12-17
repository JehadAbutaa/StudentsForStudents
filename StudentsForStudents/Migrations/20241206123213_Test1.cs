using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace StudentsForStudents.Migrations
{
    /// <inheritdoc />
    public partial class Test1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 6, 15, 32, 13, 135, DateTimeKind.Local).AddTicks(8826),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 3, 22, 16, 37, 783, DateTimeKind.Local).AddTicks(2706));

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    ImgPath = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CoursesTeacher",
                columns: table => new
                {
                    InrolmentCoursesId = table.Column<int>(type: "int", nullable: false),
                    TeachersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesTeacher", x => new { x.InrolmentCoursesId, x.TeachersId });
                    table.ForeignKey(
                        name: "FK_CoursesTeacher_Courses_InrolmentCoursesId",
                        column: x => x.InrolmentCoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursesTeacher_Teacher_TeachersId",
                        column: x => x.TeachersId,
                        principalTable: "Teacher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesTeacher_TeachersId",
                table: "CoursesTeacher",
                column: "TeachersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesTeacher");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTime",
                table: "Chats",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2024, 12, 3, 22, 16, 37, 783, DateTimeKind.Local).AddTicks(2706),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValue: new DateTime(2024, 12, 6, 15, 32, 13, 135, DateTimeKind.Local).AddTicks(8826));
        }
    }
}
