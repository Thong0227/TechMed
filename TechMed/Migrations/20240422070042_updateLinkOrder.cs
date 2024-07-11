using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMed.Migrations
{
    public partial class updateLinkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Recruitment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Recruitment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "News",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "News",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Banner",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Banner",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Banner");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Banner");
        }
    }
}
