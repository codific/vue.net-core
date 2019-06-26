using Microsoft.EntityFrameworkCore.Migrations;

namespace Application.Migrations
{
    public partial class add_winner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlayerWin",
                table: "Games",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlayerWin",
                table: "Games");
        }
    }
}
