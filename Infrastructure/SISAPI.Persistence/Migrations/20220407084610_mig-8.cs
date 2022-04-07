using Microsoft.EntityFrameworkCore.Migrations;

namespace SISAPI.Persistence.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonDetails",
                columns: table => new
                {
                    LessonId = table.Column<short>(type: "smallint", nullable: false),
                    MidtermPercentage = table.Column<short>(type: "smallint", nullable: false, defaultValue: 40),
                    FinalPercentage = table.Column<short>(type: "smallint", nullable: false, defaultValue: 60),
                    Certainty_of_percentages = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Certainty_of_midterm = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Certainty_of_makeup1 = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Certainty_of_final = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Certainty_of_makeup2 = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AA_startingGrade = table.Column<float>(type: "real", nullable: false, defaultValue: 90),
                    BA_startingGrade = table.Column<float>(type: "real", nullable: false, defaultValue: 80),
                    BB_startingGrade = table.Column<float>(type: "real", nullable: false, defaultValue: 70),
                    CB_startingGrade = table.Column<float>(type: "real", nullable: false, defaultValue: 60),
                    CC_startingGrade = table.Column<float>(type: "real", nullable: false, defaultValue: 50)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonDetails", x => x.LessonId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonDetails");
        }
    }
}
