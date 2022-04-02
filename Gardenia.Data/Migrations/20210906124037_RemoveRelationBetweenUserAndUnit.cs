using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class RemoveRelationBetweenUserAndUnit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Units_UnitID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UnitID",
                table: "AspNetUsers",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_UnitID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Units_UnitId",
                table: "AspNetUsers",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Units_UnitId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "AspNetUsers",
                newName: "UnitID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_UnitId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_UnitID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Units_UnitID",
                table: "AspNetUsers",
                column: "UnitID",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
