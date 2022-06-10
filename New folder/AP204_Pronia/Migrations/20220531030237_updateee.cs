using Microsoft.EntityFrameworkCore.Migrations;

namespace AP204_Pronia.Migrations
{
    public partial class updateee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCatagories_Catagories_CatagoryId",
                table: "PlantCatagories");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantCatagories_Plants_PlantId",
                table: "PlantCatagories");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "PlantCatagories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CatagoryId",
                table: "PlantCatagories",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCatagories_Catagories_CatagoryId",
                table: "PlantCatagories",
                column: "CatagoryId",
                principalTable: "Catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCatagories_Plants_PlantId",
                table: "PlantCatagories",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlantCatagories_Catagories_CatagoryId",
                table: "PlantCatagories");

            migrationBuilder.DropForeignKey(
                name: "FK_PlantCatagories_Plants_PlantId",
                table: "PlantCatagories");

            migrationBuilder.AlterColumn<int>(
                name: "PlantId",
                table: "PlantCatagories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CatagoryId",
                table: "PlantCatagories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCatagories_Catagories_CatagoryId",
                table: "PlantCatagories",
                column: "CatagoryId",
                principalTable: "Catagories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlantCatagories_Plants_PlantId",
                table: "PlantCatagories",
                column: "PlantId",
                principalTable: "Plants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
