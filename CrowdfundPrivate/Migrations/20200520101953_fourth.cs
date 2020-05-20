using Microsoft.EntityFrameworkCore.Migrations;

namespace Crowdfund.Migrations
{
    public partial class fourth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoLink",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "VideoLink",
                table: "Media");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Media",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MediaLink",
                table: "Media",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "MediaLink",
                table: "Media");

            migrationBuilder.AddColumn<string>(
                name: "PhotoLink",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoLink",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
