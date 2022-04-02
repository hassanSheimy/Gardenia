using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddProgressBarsCounters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoneObservationCounter",
                table: "Compounds",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "DoneUnitsCounter",
                table: "Compounds",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ObservationCounter",
                table: "Compounds",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "UnitsCounter",
                table: "Compounds",
                type: "int",
                nullable: false,
                defaultValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoneObservationCounter",
                table: "Compounds");

            migrationBuilder.DropColumn(
                name: "DoneUnitsCounter",
                table: "Compounds");

            migrationBuilder.DropColumn(
                name: "ObservationCounter",
                table: "Compounds");

            migrationBuilder.DropColumn(
                name: "UnitsCounter",
                table: "Compounds");
        }
    }
}
