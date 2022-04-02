using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddacknowledgementStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ObservationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitObservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObservationTypeID = table.Column<int>(type: "int", nullable: false),
                    UnitID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitObservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitObservations_ObservationTypes_ObservationTypeID",
                        column: x => x.ObservationTypeID,
                        principalTable: "ObservationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitObservations_Units_UnitID",
                        column: x => x.UnitID,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservationImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObservationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservationImages_UnitObservations_ObservationID",
                        column: x => x.ObservationID,
                        principalTable: "UnitObservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservationResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    ObservationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservationResponses_UnitObservations_ObservationID",
                        column: x => x.ObservationID,
                        principalTable: "UnitObservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ObservationResponseImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespnseImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObservationResponseImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObservationResponseImages_ObservationResponses_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "ObservationResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ObservationImages_ObservationID",
                table: "ObservationImages",
                column: "ObservationID");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationResponseImages_ResponseID",
                table: "ObservationResponseImages",
                column: "ResponseID");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationResponses_ObservationID",
                table: "ObservationResponses",
                column: "ObservationID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitObservations_ObservationTypeID",
                table: "UnitObservations",
                column: "ObservationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_UnitObservations_UnitID",
                table: "UnitObservations",
                column: "UnitID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ObservationImages");

            migrationBuilder.DropTable(
                name: "ObservationResponseImages");

            migrationBuilder.DropTable(
                name: "ObservationResponses");

            migrationBuilder.DropTable(
                name: "UnitObservations");

            migrationBuilder.DropTable(
                name: "ObservationTypes");
        }
    }
}
