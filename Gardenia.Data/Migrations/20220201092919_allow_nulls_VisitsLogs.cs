using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class allow_nulls_VisitsLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitsLogs_QRVisitorInvitation_QRVisitorInvitationId",
                table: "VisitsLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitsLogs_SMSVisitorInvitation_SMSVisitorInvitationId",
                table: "VisitsLogs");

            migrationBuilder.AlterColumn<int>(
                name: "SMSVisitorInvitationId",
                table: "VisitsLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "QRVisitorInvitationId",
                table: "VisitsLogs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VisitsLogs_QRVisitorInvitation_QRVisitorInvitationId",
                table: "VisitsLogs",
                column: "QRVisitorInvitationId",
                principalTable: "QRVisitorInvitation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitsLogs_SMSVisitorInvitation_SMSVisitorInvitationId",
                table: "VisitsLogs",
                column: "SMSVisitorInvitationId",
                principalTable: "SMSVisitorInvitation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VisitsLogs_QRVisitorInvitation_QRVisitorInvitationId",
                table: "VisitsLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_VisitsLogs_SMSVisitorInvitation_SMSVisitorInvitationId",
                table: "VisitsLogs");

            migrationBuilder.AlterColumn<int>(
                name: "SMSVisitorInvitationId",
                table: "VisitsLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QRVisitorInvitationId",
                table: "VisitsLogs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitsLogs_QRVisitorInvitation_QRVisitorInvitationId",
                table: "VisitsLogs",
                column: "QRVisitorInvitationId",
                principalTable: "QRVisitorInvitation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VisitsLogs_SMSVisitorInvitation_SMSVisitorInvitationId",
                table: "VisitsLogs",
                column: "SMSVisitorInvitationId",
                principalTable: "SMSVisitorInvitation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
