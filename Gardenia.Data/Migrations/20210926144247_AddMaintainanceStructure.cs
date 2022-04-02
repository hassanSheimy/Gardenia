using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddMaintainanceStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maintainances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportTypeID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintainances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintainances_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Maintainances_MaintainanceTypes_ReportTypeID",
                        column: x => x.ReportTypeID,
                        principalTable: "MaintainanceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintainances_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintainanceImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintainanceImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintainanceImages_Maintainances_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Maintainances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintainanceResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    ReportID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintainanceResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintainanceResponses_Maintainances_ReportID",
                        column: x => x.ReportID,
                        principalTable: "Maintainances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaintainanceResponseImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespnseImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintainanceResponseImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaintainanceResponseImages_MaintainanceResponses_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "MaintainanceResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaintainanceImages_ReportID",
                table: "MaintainanceImages",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintainanceResponseImages_ResponseID",
                table: "MaintainanceResponseImages",
                column: "ResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintainanceResponses_ReportID",
                table: "MaintainanceResponses",
                column: "ReportID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainances_ReportTypeID",
                table: "Maintainances",
                column: "ReportTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainances_UnitID",
                table: "Maintainances",
                column: "UnitID");

            migrationBuilder.CreateIndex(
                name: "IX_Maintainances_UserID",
                table: "Maintainances",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MaintainanceImages");

            migrationBuilder.DropTable(
                name: "MaintainanceResponseImages");

            migrationBuilder.DropTable(
                name: "MaintainanceResponses");

            migrationBuilder.DropTable(
                name: "Maintainances");
        }
    }
}
