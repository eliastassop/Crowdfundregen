using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Core.Migrations
{
    public partial class creatorid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reward_Project_ProjectId",
                table: "Reward");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusUpdate_Project_ProjectId",
                table: "StatusUpdate");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "StatusUpdate",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Reward",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reward_Project_ProjectId",
                table: "Reward",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusUpdate_Project_ProjectId",
                table: "StatusUpdate",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reward_Project_ProjectId",
                table: "Reward");

            migrationBuilder.DropForeignKey(
                name: "FK_StatusUpdate_Project_ProjectId",
                table: "StatusUpdate");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Project");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "StatusUpdate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Reward",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Reward_Project_ProjectId",
                table: "Reward",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StatusUpdate_Project_ProjectId",
                table: "StatusUpdate",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
