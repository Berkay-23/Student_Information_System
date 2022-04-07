using Microsoft.EntityFrameworkCore.Migrations;

namespace SISAPI.Persistence.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");
        }
    }
}
