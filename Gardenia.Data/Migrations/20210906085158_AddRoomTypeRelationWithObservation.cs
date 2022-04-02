using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddRoomTypeRelationWithObservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomTypeID",
                table: "UnitObservations",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_UnitObservations_RoomTypeID",
                table: "UnitObservations",
                column: "RoomTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_UnitObservations_RoomTypes_RoomTypeID",
                table: "UnitObservations",
                column: "RoomTypeID",
                principalTable: "RoomTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UnitObservations_RoomTypes_RoomTypeID",
                table: "UnitObservations");

            migrationBuilder.DropIndex(
                name: "IX_UnitObservations_RoomTypeID",
                table: "UnitObservations");

            migrationBuilder.DropColumn(
                name: "RoomTypeID",
                table: "UnitObservations");
        }
    }
}
