using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class addinvitation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSVisitorInvitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsSMSVisitor = table.Column<bool>(type: "bit", nullable: false),
                    NameOrDesc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSVisitorInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMSVisitorInvitation_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Visitor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QRVisitorInvitation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRVisitorInvitation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QRVisitorInvitation_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QRVisitorInvitation_Visitor_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VisitsLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SecurityId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QRVisitorInvitationId = table.Column<int>(type: "int", nullable: false),
                    SMSVisitorInvitationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitsLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitsLogs_AspNetUsers_SecurityId",
                        column: x => x.SecurityId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VisitsLogs_QRVisitorInvitation_QRVisitorInvitationId",
                        column: x => x.QRVisitorInvitationId,
                        principalTable: "QRVisitorInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VisitsLogs_SMSVisitorInvitation_SMSVisitorInvitationId",
                        column: x => x.SMSVisitorInvitationId,
                        principalTable: "SMSVisitorInvitation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkImageID",
                columns: table => new
                {
                    ImagePath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VisitsLogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkImageID", x => new { x.ImagePath, x.VisitsLogId });
                    table.ForeignKey(
                        name: "FK_WorkImageID_VisitsLogs_VisitsLogId",
                        column: x => x.VisitsLogId,
                        principalTable: "VisitsLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QRVisitorInvitation_OwnerId",
                table: "QRVisitorInvitation",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_QRVisitorInvitation_VisitorId",
                table: "QRVisitorInvitation",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_SMSVisitorInvitation_OwnerId",
                table: "SMSVisitorInvitation",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitsLogs_QRVisitorInvitationId",
                table: "VisitsLogs",
                column: "QRVisitorInvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitsLogs_SecurityId",
                table: "VisitsLogs",
                column: "SecurityId");

            migrationBuilder.CreateIndex(
                name: "IX_VisitsLogs_SMSVisitorInvitationId",
                table: "VisitsLogs",
                column: "SMSVisitorInvitationId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkImageID_VisitsLogId",
                table: "WorkImageID",
                column: "VisitsLogId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkImageID");

            migrationBuilder.DropTable(
                name: "VisitsLogs");

            migrationBuilder.DropTable(
                name: "QRVisitorInvitation");

            migrationBuilder.DropTable(
                name: "SMSVisitorInvitation");

            migrationBuilder.DropTable(
                name: "Visitor");
        }
    }
}
