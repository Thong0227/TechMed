using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMed.Migrations
{
    public partial class updatemenu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Link",
                table: "Menu",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "Menu");
        }
    }
}
