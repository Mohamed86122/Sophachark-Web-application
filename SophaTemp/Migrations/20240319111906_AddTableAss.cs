using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddTableAss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MedicamentCategoryMedicament",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    CategoryMedicamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentCategoryMedicament", x => new { x.MedicamentId, x.CategoryMedicamentId });
                    table.ForeignKey(
                        name: "FK_MedicamentCategoryMedicament_CategoryMedicament_CategoryMedicamentId",
                        column: x => x.CategoryMedicamentId,
                        principalTable: "CategoryMedicament",
                        principalColumn: "CategoryMedicamentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentCategoryMedicament_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentCategoryMedicament_CategoryMedicamentId",
                table: "MedicamentCategoryMedicament",
                column: "CategoryMedicamentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicamentCategoryMedicament");
        }
    }
}
