using Microsoft.EntityFrameworkCore.Migrations;

namespace SISAPI.Persistence.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Academics");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Academics",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
