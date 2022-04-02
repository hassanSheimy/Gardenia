using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddRelationUnitAndFloar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FloarNumber",
                table: "Units");

            migrationBuilder.AddColumn<int>(
                name: "FloarID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Units_FloarID",
                table: "Units",
                column: "FloarID");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Floars_FloarID",
                table: "Units",
                column: "FloarID",
                principalTable: "Floars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Floars_FloarID",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_FloarID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "FloarID",
                table: "Units");

            migrationBuilder.AddColumn<string>(
                name: "FloarNumber",
                table: "Units",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
