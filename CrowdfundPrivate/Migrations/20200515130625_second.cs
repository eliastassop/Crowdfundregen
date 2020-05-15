using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "StatusUpdate",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusUpdate_ProjectId",
                table: "StatusUpdate",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusUpdate_Project_ProjectId",
                table: "StatusUpdate",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusUpdate_Project_ProjectId",
                table: "StatusUpdate");

            migrationBuilder.DropIndex(
                name: "IX_StatusUpdate_ProjectId",
                table: "StatusUpdate");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "StatusUpdate");
        }
    }
}
