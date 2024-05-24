using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class UpdatePropertyFromModelToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_LotCommandes_LotCommandeId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_LotCommandes_LotCommandes_LotCommandeId1",
                table: "LotCommandes");

            migrationBuilder.DropIndex(
                name: "IX_LotCommandes_LotCommandeId1",
                table: "LotCommandes");

            migrationBuilder.DropIndex(
                name: "IX_Commandes_LotCommandeId",
                table: "Commandes");

            migrationBuilder.DropColumn(
                name: "LotCommandeId1",
                table: "LotCommandes");

            migrationBuilder.DropColumn(
                name: "LotCommandeId",
                table: "Commandes");

            migrationBuilder.AddColumn<int>(
                name: "CommandeId",
                table: "LotCommandes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LotCommandes_CommandeId",
                table: "LotCommandes",
                column: "CommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_LotCommandes_Commandes_CommandeId",
                table: "LotCommandes",
                column: "CommandeId",
                principalTable: "Commandes",
                principalColumn: "CommandeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotCommandes_Commandes_CommandeId",
                table: "LotCommandes");

            migrationBuilder.DropIndex(
                name: "IX_LotCommandes_CommandeId",
                table: "LotCommandes");

            migrationBuilder.DropColumn(
                name: "CommandeId",
                table: "LotCommandes");

            migrationBuilder.AddColumn<int>(
                name: "LotCommandeId1",
                table: "LotCommandes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LotCommandeId",
                table: "Commandes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LotCommandes_LotCommandeId1",
                table: "LotCommandes",
                column: "LotCommandeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_LotCommandeId",
                table: "Commandes",
                column: "LotCommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_LotCommandes_LotCommandeId",
                table: "Commandes",
                column: "LotCommandeId",
                principalTable: "LotCommandes",
                principalColumn: "LotCommandeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LotCommandes_LotCommandes_LotCommandeId1",
                table: "LotCommandes",
                column: "LotCommandeId1",
                principalTable: "LotCommandes",
                principalColumn: "LotCommandeId");
        }
    }
}
