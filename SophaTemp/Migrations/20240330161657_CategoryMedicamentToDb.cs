using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class CategoryMedicamentToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicamentCategoryMedicament_CategoryMedicament_CategoryMedicamentId",
                table: "MedicamentCategoryMedicament");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicamentCategoryMedicament_Medicaments_MedicamentId",
                table: "MedicamentCategoryMedicament");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicamentCategoryMedicament",
                table: "MedicamentCategoryMedicament");

            migrationBuilder.RenameTable(
                name: "MedicamentCategoryMedicament",
                newName: "categoryMedicaments");

            migrationBuilder.RenameIndex(
                name: "IX_MedicamentCategoryMedicament_CategoryMedicamentId",
                table: "categoryMedicaments",
                newName: "IX_categoryMedicaments_CategoryMedicamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categoryMedicaments",
                table: "categoryMedicaments",
                columns: new[] { "MedicamentId", "CategoryMedicamentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_categoryMedicaments_CategoryMedicament_CategoryMedicamentId",
                table: "categoryMedicaments",
                column: "CategoryMedicamentId",
                principalTable: "CategoryMedicament",
                principalColumn: "CategoryMedicamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categoryMedicaments_Medicaments_MedicamentId",
                table: "categoryMedicaments",
                column: "MedicamentId",
                principalTable: "Medicaments",
                principalColumn: "MedicamentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoryMedicaments_CategoryMedicament_CategoryMedicamentId",
                table: "categoryMedicaments");

            migrationBuilder.DropForeignKey(
                name: "FK_categoryMedicaments_Medicaments_MedicamentId",
                table: "categoryMedicaments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categoryMedicaments",
                table: "categoryMedicaments");

            migrationBuilder.RenameTable(
                name: "categoryMedicaments",
                newName: "MedicamentCategoryMedicament");

            migrationBuilder.RenameIndex(
                name: "IX_categoryMedicaments_CategoryMedicamentId",
                table: "MedicamentCategoryMedicament",
                newName: "IX_MedicamentCategoryMedicament_CategoryMedicamentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicamentCategoryMedicament",
                table: "MedicamentCategoryMedicament",
                columns: new[] { "MedicamentId", "CategoryMedicamentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicamentCategoryMedicament_CategoryMedicament_CategoryMedicamentId",
                table: "MedicamentCategoryMedicament",
                column: "CategoryMedicamentId",
                principalTable: "CategoryMedicament",
                principalColumn: "CategoryMedicamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicamentCategoryMedicament_Medicaments_MedicamentId",
                table: "MedicamentCategoryMedicament",
                column: "MedicamentId",
                principalTable: "Medicaments",
                principalColumn: "MedicamentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
