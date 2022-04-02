using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class oneUserManyUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Units",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Units_AppUserId",
                table: "Units",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_AspNetUsers_AppUserId",
                table: "Units",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_AspNetUsers_AppUserId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_AppUserId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Units");
        }
    }
}
