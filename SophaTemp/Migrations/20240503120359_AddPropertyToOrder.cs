using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddPropertyToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Commandes_MedicamentId",
                table: "Commandes",
                column: "MedicamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Medicaments_MedicamentId",
                table: "Commandes",
                column: "MedicamentId",
                principalTable: "Medicaments",
                principalColumn: "MedicamentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Medicaments_MedicamentId",
                table: "Commandes");

            migrationBuilder.DropIndex(
                name: "IX_Commandes_MedicamentId",
                table: "Commandes");
        }
    }
}
