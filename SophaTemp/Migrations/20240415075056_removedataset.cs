using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class removedataset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoryMedicaments_CategoryMedicament_CategoryMedicamentId",
                table: "categoryMedicaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryMedicament",
                table: "CategoryMedicament");

            migrationBuilder.RenameTable(
                name: "CategoryMedicament",
                newName: "Categories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "CategoryMedicamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_categoryMedicaments_Categories_CategoryMedicamentId",
                table: "categoryMedicaments",
                column: "CategoryMedicamentId",
                principalTable: "Categories",
                principalColumn: "CategoryMedicamentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoryMedicaments_Categories_CategoryMedicamentId",
                table: "categoryMedicaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "CategoryMedicament");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryMedicament",
                table: "CategoryMedicament",
                column: "CategoryMedicamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_categoryMedicaments_CategoryMedicament_CategoryMedicamentId",
                table: "categoryMedicaments",
                column: "CategoryMedicamentId",
                principalTable: "CategoryMedicament",
                principalColumn: "CategoryMedicamentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
