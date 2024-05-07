using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddIdClientToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Clients",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Medicaments_MedicamentId",
                table: "Commandes");

            migrationBuilder.DropIndex(
                name: "IX_Commandes_MedicamentId",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "MedicamentId",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Libelle",
                table: "LotCommandes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
