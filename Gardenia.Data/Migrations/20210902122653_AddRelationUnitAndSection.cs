using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddRelationUnitAndSection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionID",
                table: "Units",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Units_SectionID",
                table: "Units",
                column: "SectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_Sections_SectionID",
                table: "Units",
                column: "SectionID",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_Sections_SectionID",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_SectionID",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "SectionID",
                table: "Units");
        }
    }
}
