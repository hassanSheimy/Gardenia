using Microsoft.EntityFrameworkCore.Migrations;

namespace Gardenia.Data.Migrations
{
    public partial class AddOrderResponseImagesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderResponseImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RespnseImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderResponseImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderResponseImages_OrderResponses_ResponseID",
                        column: x => x.ResponseID,
                        principalTable: "OrderResponses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderResponseImages_ResponseID",
                table: "OrderResponseImages",
                column: "ResponseID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderResponseImages");
        }
    }
}
