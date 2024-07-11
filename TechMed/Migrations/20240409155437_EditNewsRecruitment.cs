using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMed.Migrations
{
    public partial class EditNewsRecruitment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Recruitment",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Recruitment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Recruitment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "News",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "News",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Recruitment");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "News");
        }
    }
}
