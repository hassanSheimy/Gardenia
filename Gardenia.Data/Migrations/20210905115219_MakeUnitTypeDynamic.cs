using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class MakeUnitTypeDynamic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UnitTypeID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitTypeID",
                table: "Units",
                column: "UnitTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeID",
                table: "Units",
                column: "UnitTypeID",
                principalTable: "UnitTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_UnitTypes_UnitTypeID",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_UnitTypeID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "UnitTypeID",
                table: "Units");
        }
    }
}
