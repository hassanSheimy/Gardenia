using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddRelationWithUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitID",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UnitID",
                table: "AspNetUsers",
                column: "UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Units_UnitID",
                table: "AspNetUsers",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Units_UnitID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UnitID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UnitID",
                table: "AspNetUsers");
        }
    }
}
