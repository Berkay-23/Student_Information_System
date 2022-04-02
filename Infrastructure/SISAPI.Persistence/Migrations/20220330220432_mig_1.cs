using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SISAPI.Persistence.Migrations
{
    public partial class mig_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Absenteeism",
                columns: table => new
                {
                    StudentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonId = table.Column<short>(type: "smallint", nullable: false),
                    TheoreticalAbs = table.Column<byte>(type: "tinyint", nullable: false),
                    PracticalAbs = table.Column<byte>(type: "tinyint", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "(getdate())"),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Academics",
                columns: table => new
                {
                    AcademicianId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Academics", x => x.AcademicianId);
                });

            migrationBuilder.CreateTable(
                name: "GradeInformations",
                columns: table => new
                {
                    StudentNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Gpas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalEcts = table.Column<short>(type: "smallint", nullable: false),
                    TranscriptNote = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeInformations", x => x.StudentNo);
                });

            migrationBuilder.CreateTable(
                name: "LessonInformations",
                columns: table => new
                {
                    StudentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredCourses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetakeFailCourses = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ects = table.Column<byte>(type: "tinyint", nullable: false),
                    Credit = table.Column<byte>(type: "tinyint", nullable: false),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    PraticalLimit = table.Column<byte>(type: "tinyint", nullable: false),
                    TheoreticalLimit = table.Column<byte>(type: "tinyint", nullable: false),
                    LecturerId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    StudentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonId = table.Column<short>(type: "smallint", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MidtermExam = table.Column<double>(type: "float", nullable: true),
                    MakeUpExam1 = table.Column<double>(type: "float", nullable: true),
                    FinalExam = table.Column<double>(type: "float", nullable: true),
                    MakeUpExam2 = table.Column<double>(type: "float", nullable: true),
                    Average = table.Column<double>(type: "float", nullable: true),
                    LetterScore = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    student_no = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeLevel = table.Column<byte>(type: "tinyint", nullable: false),
                    AdvisorId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.student_no);
                });

            migrationBuilder.CreateTable(
                name: "UnsuccessfulStudents",
                columns: table => new
                {
                    StudentNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonId = table.Column<short>(type: "smallint", nullable: false),
                    Period = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfFailure = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absenteeism");

            migrationBuilder.DropTable(
                name: "Academics");

            migrationBuilder.DropTable(
                name: "GradeInformations");

            migrationBuilder.DropTable(
                name: "LessonInformations");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "UnsuccessfulStudents");
        }
    }
}
