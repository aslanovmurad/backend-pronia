using Microsoft.EntityFrameworkCore.Migrations;

namespace AP204_Pronia.Migrations
{
    public partial class newtible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Catagories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catagories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlantCatagories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatagoryId = table.Column<int>(nullable: true),
                    PlantId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCatagories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantCatagories_Catagories_CatagoryId",
                        column: x => x.CatagoryId,
                        principalTable: "Catagories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantCatagories_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlantCatagories_CatagoryId",
                table: "PlantCatagories",
                column: "CatagoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCatagories_PlantId",
                table: "PlantCatagories",
                column: "PlantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlantCatagories");

            migrationBuilder.DropTable(
                name: "Catagories");
        }
    }
}
