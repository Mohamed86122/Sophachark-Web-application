using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddColumnLibelleTOLOTCOMMANDE : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            

            migrationBuilder.AddColumn<string>(
                name: "Libelle",
                table: "LotCommandes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Libelle",
                table: "LotCommandes");

            migrationBuilder.AddColumn<int>(
                name: "MedicamentId",
                table: "Commandes",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
