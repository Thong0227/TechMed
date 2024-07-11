using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMed.Migrations
{
    public partial class removeThumbnail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "News");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
