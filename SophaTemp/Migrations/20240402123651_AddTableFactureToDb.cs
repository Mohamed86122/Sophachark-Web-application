using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddTableFactureToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnGarde",
                table: "Personnes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "X",
                table: "Personnes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Y",
                table: "Personnes",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    FactureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Montant = table.Column<double>(type: "float", nullable: false),
                    DateFacturation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.FactureId);
                    table.ForeignKey(
                        name: "FK_Factures_Commandes_CommandeId",
                        column: x => x.CommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CommandeId",
                table: "Factures",
                column: "CommandeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "EnGarde",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Personnes");
        }
    }
}
