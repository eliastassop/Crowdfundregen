using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Backer",
                columns: table => new
                {
                    BackerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backer", x => x.BackerId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCreator",
                columns: table => new
                {
                    ProjectCreatorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(nullable: true),
                    UserCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCreator", x => x.ProjectCreatorId);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CurrentFund = table.Column<decimal>(nullable: false),
                    TotalFund = table.Column<decimal>(nullable: false),
                    VideoLink = table.Column<string>(nullable: true),
                    PhotoLink = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    ProjectCreated = table.Column<DateTime>(nullable: false),
                    ProjectDeadline = table.Column<DateTime>(nullable: false),
                    ProjectCreatorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_ProjectCreator_ProjectCreatorId",
                        column: x => x.ProjectCreatorId,
                        principalTable: "ProjectCreator",
                        principalColumn: "ProjectCreatorId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reward",
                columns: table => new
                {
                    RewardId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reward", x => x.RewardId);
                    table.ForeignKey(
                        name: "FK_Reward_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RewardBacker",
                columns: table => new
                {
                    BackerId = table.Column<int>(nullable: false),
                    RewardId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RewardBacker", x => new { x.BackerId, x.RewardId });
                    table.ForeignKey(
                        name: "FK_RewardBacker_Backer_BackerId",
                        column: x => x.BackerId,
                        principalTable: "Backer",
                        principalColumn: "BackerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RewardBacker_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RewardBacker_Reward_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Reward",
                        principalColumn: "RewardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectCreatorId",
                table: "Project",
                column: "ProjectCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reward_ProjectId",
                table: "Reward",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardBacker_ProjectId",
                table: "RewardBacker",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardBacker_RewardId",
                table: "RewardBacker",
                column: "RewardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RewardBacker");

            migrationBuilder.DropTable(
                name: "Backer");

            migrationBuilder.DropTable(
                name: "Reward");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "ProjectCreator");
        }
    }
}
