using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechMed.Migrations
{
    public partial class UpdateCanBeNUllCateRecruitment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_RecruitmentCate_RecruitmentCateId",
                table: "Recruitment");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecruitmentCateId",
                table: "Recruitment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_RecruitmentCate_RecruitmentCateId",
                table: "Recruitment",
                column: "RecruitmentCateId",
                principalTable: "RecruitmentCate",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recruitment_RecruitmentCate_RecruitmentCateId",
                table: "Recruitment");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecruitmentCateId",
                table: "Recruitment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recruitment_RecruitmentCate_RecruitmentCateId",
                table: "Recruitment",
                column: "RecruitmentCateId",
                principalTable: "RecruitmentCate",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
