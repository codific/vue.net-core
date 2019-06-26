using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class winning_combination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinningCombination",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinningCombination",
                table: "Games");
        }
    }
}
