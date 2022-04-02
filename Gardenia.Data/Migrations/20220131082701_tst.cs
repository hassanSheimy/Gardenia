using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class tst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QRVisitorInvitation_Visitor_VisitorId",
                table: "QRVisitorInvitation");

            migrationBuilder.AlterColumn<int>(
                name: "VisitorId",
                table: "QRVisitorInvitation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "QRVisitorInvitation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QRVisitorInvitation_Visitor_VisitorId",
                table: "QRVisitorInvitation",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QRVisitorInvitation_Visitor_VisitorId",
                table: "QRVisitorInvitation");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "QRVisitorInvitation");

            migrationBuilder.AlterColumn<int>(
                name: "VisitorId",
                table: "QRVisitorInvitation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QRVisitorInvitation_Visitor_VisitorId",
                table: "QRVisitorInvitation",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
