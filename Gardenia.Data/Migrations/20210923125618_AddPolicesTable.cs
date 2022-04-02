using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddPolicesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Polices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    RatersCount = table.Column<int>(type: "int", nullable: false),
                    Late = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Long = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaderAssistantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimiLeaderAssistatnName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaderPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaderAssistantPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SimiLeaderAssistatnPhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Polices");
        }
    }
}
