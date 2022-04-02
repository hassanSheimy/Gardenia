using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class RenameUserIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserIdentities_UserIdentityIDId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserIdentity",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserIdentityIDId",
                table: "AspNetUsers",
                newName: "UserIdentityID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_UserIdentityIDId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_UserIdentityID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserIdentities_UserIdentityID",
                table: "AspNetUsers",
                column: "UserIdentityID",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_UserIdentities_UserIdentityID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserIdentityID",
                table: "AspNetUsers",
                newName: "UserIdentityIDId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_UserIdentityID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_UserIdentityIDId");

            migrationBuilder.AddColumn<int>(
                name: "UserIdentity",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_UserIdentities_UserIdentityIDId",
                table: "AspNetUsers",
                column: "UserIdentityIDId",
                principalTable: "UserIdentities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
