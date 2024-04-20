using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class UpdateTbMedicamentcat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryMedicamentMedicament");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryMedicamentMedicament",
                columns: table => new
                {
                    CategoriesCategoryMedicamentId = table.Column<int>(type: "int", nullable: false),
                    MedicamentsMedicamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryMedicamentMedicament", x => new { x.CategoriesCategoryMedicamentId, x.MedicamentsMedicamentId });
                    table.ForeignKey(
                        name: "FK_CategoryMedicamentMedicament_CategoryMedicament_CategoriesCategoryMedicamentId",
                        column: x => x.CategoriesCategoryMedicamentId,
                        principalTable: "CategoryMedicament",
                        principalColumn: "CategoryMedicamentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryMedicamentMedicament_Medicaments_MedicamentsMedicamentId",
                        column: x => x.MedicamentsMedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryMedicamentMedicament_MedicamentsMedicamentId",
                table: "CategoryMedicamentMedicament",
                column: "MedicamentsMedicamentId");
        }
    }
}
