using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class qrinvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "QRVisitorInvitation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_QRVisitorInvitation_Visitor_VisitorId",
                table: "QRVisitorInvitation",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "QRVisitorInvitation",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "QRVisitorInvitation",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_QRVisitorInvitation_Visitor_VisitorId",
                table: "QRVisitorInvitation",
                column: "VisitorId",
                principalTable: "Visitor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
