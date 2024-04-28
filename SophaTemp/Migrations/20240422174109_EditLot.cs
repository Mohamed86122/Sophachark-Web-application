using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class EditLot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LotCommandes_Lots_LotId",
                table: "LotCommandes");

            migrationBuilder.DropIndex(
                name: "IX_LotCommandes_LotId",
                table: "LotCommandes");

            migrationBuilder.DropColumn(
                name: "LotId",
                table: "LotCommandes");

            migrationBuilder.AddColumn<int>(
                name: "LotCommandeId",
                table: "Lots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lots_LotCommandeId",
                table: "Lots",
                column: "LotCommandeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lots_LotCommandes_LotCommandeId",
                table: "Lots",
                column: "LotCommandeId",
                principalTable: "LotCommandes",
                principalColumn: "LotCommandeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lots_LotCommandes_LotCommandeId",
                table: "Lots");

            migrationBuilder.DropIndex(
                name: "IX_Lots_LotCommandeId",
                table: "Lots");

            migrationBuilder.DropColumn(
                name: "LotCommandeId",
                table: "Lots");

            migrationBuilder.AddColumn<int>(
                name: "LotId",
                table: "LotCommandes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LotCommandes_LotId",
                table: "LotCommandes",
                column: "LotId");

            migrationBuilder.AddForeignKey(
                name: "FK_LotCommandes_Lots_LotId",
                table: "LotCommandes",
                column: "LotId",
                principalTable: "Lots",
                principalColumn: "LotId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
