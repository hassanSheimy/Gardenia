using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddRelationWithUserIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserIdentity",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "UserIdentityIDId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserIdentityIDId",
                table: "AspNetUsers",
                column: "UserIdentityIDId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserIdentities_UserIdentityIDId",
                table: "AspNetUsers",
                column: "UserIdentityIDId",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserIdentities_UserIdentityIDId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserIdentityIDId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserIdentity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserIdentityIDId",
                table: "AspNetUsers");
        }
    }
}
