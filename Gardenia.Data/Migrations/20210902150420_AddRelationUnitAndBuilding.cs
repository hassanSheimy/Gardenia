using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddRelationUnitAndBuilding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BuildingID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Units_BuildingID",
                table: "Units",
                column: "BuildingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Buildings_BuildingID",
                table: "Units",
                column: "BuildingID",
                principalTable: "Buildings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Buildings_BuildingID",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_BuildingID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "BuildingID",
                table: "Units");
        }
    }
}
