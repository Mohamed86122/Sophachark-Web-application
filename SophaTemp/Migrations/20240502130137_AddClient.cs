using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Personnes_ClientPersonneId",
                table: "Commandes");

            migrationBuilder.DropForeignKey(
                name: "FK_Personnes_Whishlists_WhishlistId",
                table: "Personnes");

            migrationBuilder.DropIndex(
                name: "IX_Personnes_WhishlistId",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Adresse",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "EnGarde",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "LibellePharmacie",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Telephone",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Ville",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "WhishlistId",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Personnes");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Personnes");

            migrationBuilder.CreateTable(
                name: "AdminCommandes",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminCommandes", x => x.PersonneId);
                    table.ForeignKey(
                        name: "FK_AdminCommandes_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId");
                });

            migrationBuilder.CreateTable(
                name: "AdminPrincipals",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminPrincipals", x => x.PersonneId);
                    table.ForeignKey(
                        name: "FK_AdminPrincipals_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId");
                });

            migrationBuilder.CreateTable(
                name: "AdminProduits",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProduits", x => x.PersonneId);
                    table.ForeignKey(
                        name: "FK_AdminProduits_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId");
                });

            migrationBuilder.CreateTable(
                name: "AdminStocks",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminStocks", x => x.PersonneId);
                    table.ForeignKey(
                        name: "FK_AdminStocks_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId");
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    LibellePharmacie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ville = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnGarde = table.Column<bool>(type: "bit", nullable: false),
                    WhishlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.PersonneId);
                    table.ForeignKey(
                        name: "FK_Clients_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId");
                    table.ForeignKey(
                        name: "FK_Clients_Whishlists_WhishlistId",
                        column: x => x.WhishlistId,
                        principalTable: "Whishlists",
                        principalColumn: "WhishlistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_WhishlistId",
                table: "Clients",
                column: "WhishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Clients_ClientPersonneId",
                table: "Commandes",
                column: "ClientPersonneId",
                principalTable: "Clients",
                principalColumn: "PersonneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commandes_Clients_ClientPersonneId",
                table: "Commandes");

            migrationBuilder.DropTable(
                name: "AdminCommandes");

            migrationBuilder.DropTable(
                name: "AdminPrincipals");

            migrationBuilder.DropTable(
                name: "AdminProduits");

            migrationBuilder.DropTable(
                name: "AdminStocks");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "Adresse",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Personnes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "EnGarde",
                table: "Personnes",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LibellePharmacie",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telephone",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ville",
                table: "Personnes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WhishlistId",
                table: "Personnes",
                type: "int",
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

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_WhishlistId",
                table: "Personnes",
                column: "WhishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commandes_Personnes_ClientPersonneId",
                table: "Commandes",
                column: "ClientPersonneId",
                principalTable: "Personnes",
                principalColumn: "PersonneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Personnes_Whishlists_WhishlistId",
                table: "Personnes",
                column: "WhishlistId",
                principalTable: "Whishlists",
                principalColumn: "WhishlistId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
