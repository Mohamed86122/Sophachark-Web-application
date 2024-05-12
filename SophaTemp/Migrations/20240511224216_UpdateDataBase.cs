using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class UpdateDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryMedicamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryMedicamentId);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseurs",
                columns: table => new
                {
                    FournisseurId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomComplet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseurs", x => x.FournisseurId);
                });

            migrationBuilder.CreateTable(
                name: "Livraisons",
                columns: table => new
                {
                    LivraisonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateLivraison = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livraisons", x => x.LivraisonId);
                });

            migrationBuilder.CreateTable(
                name: "LotCommandes",
                columns: table => new
                {
                    LotCommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Frais = table.Column<double>(type: "float", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotCommandes", x => x.LotCommandeId);
                });

            migrationBuilder.CreateTable(
                name: "Medicaments",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantiteEnAlerte = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicaments", x => x.MedicamentId);
                });

            migrationBuilder.CreateTable(
                name: "Passeports",
                columns: table => new
                {
                    PasseportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passeports", x => x.PasseportId);
                });

            migrationBuilder.CreateTable(
                name: "Whishlists",
                columns: table => new
                {
                    WhishlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Whishlists", x => x.WhishlistId);
                });

            migrationBuilder.CreateTable(
                name: "categoryMedicaments",
                columns: table => new
                {
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    CategoryMedicamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categoryMedicaments", x => new { x.MedicamentId, x.CategoryMedicamentId });
                    table.ForeignKey(
                        name: "FK_categoryMedicaments_Categories_CategoryMedicamentId",
                        column: x => x.CategoryMedicamentId,
                        principalTable: "Categories",
                        principalColumn: "CategoryMedicamentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_categoryMedicaments_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    LotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Montant = table.Column<double>(type: "float", nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    PrixAchat = table.Column<int>(type: "int", nullable: false),
                    PrixVente = table.Column<int>(type: "int", nullable: false),
                    DateDeProduction = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDExpedition = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    FournisseurId = table.Column<int>(type: "int", nullable: false),
                    LotCommandeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lots", x => x.LotId);
                    table.ForeignKey(
                        name: "FK_Lots_Fournisseurs_FournisseurId",
                        column: x => x.FournisseurId,
                        principalTable: "Fournisseurs",
                        principalColumn: "FournisseurId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lots_LotCommandes_LotCommandeId",
                        column: x => x.LotCommandeId,
                        principalTable: "LotCommandes",
                        principalColumn: "LotCommandeId");
                    table.ForeignKey(
                        name: "FK_Lots_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasseportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_permissions_Passeports_PasseportId",
                        column: x => x.PasseportId,
                        principalTable: "Passeports",
                        principalColumn: "PasseportId");
                });

            migrationBuilder.CreateTable(
                name: "Personnes",
                columns: table => new
                {
                    PersonneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    motdepasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasseportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnes", x => x.PersonneId);
                    table.ForeignKey(
                        name: "FK_Personnes_Passeports_PasseportId",
                        column: x => x.PasseportId,
                        principalTable: "Passeports",
                        principalColumn: "PasseportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicamentWhishlist",
                columns: table => new
                {
                    MedicamentsMedicamentId = table.Column<int>(type: "int", nullable: false),
                    WhishlistsWhishlistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicamentWhishlist", x => new { x.MedicamentsMedicamentId, x.WhishlistsWhishlistId });
                    table.ForeignKey(
                        name: "FK_MedicamentWhishlist_Medicaments_MedicamentsMedicamentId",
                        column: x => x.MedicamentsMedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicamentWhishlist_Whishlists_WhishlistsWhishlistId",
                        column: x => x.WhishlistsWhishlistId,
                        principalTable: "Whishlists",
                        principalColumn: "WhishlistId",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "Commentaire",
                columns: table => new
                {
                    CommentaireId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonneId = table.Column<int>(type: "int", nullable: false),
                    MedicamentId = table.Column<int>(type: "int", nullable: false),
                    CommentaireParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaire", x => x.CommentaireId);
                    table.ForeignKey(
                        name: "FK_Commentaire_Commentaire_CommentaireParentId",
                        column: x => x.CommentaireParentId,
                        principalTable: "Commentaire",
                        principalColumn: "CommentaireId");
                    table.ForeignKey(
                        name: "FK_Commentaire_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commentaire_Personnes_PersonneId",
                        column: x => x.PersonneId,
                        principalTable: "Personnes",
                        principalColumn: "PersonneId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commandes",
                columns: table => new
                {
                    CommandeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    DateCommande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantite = table.Column<int>(type: "int", nullable: false),
                    IdLotCommande = table.Column<int>(type: "int", nullable: false),
                    LotCommandeId = table.Column<int>(type: "int", nullable: false),
                    MedicamentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commandes", x => x.CommandeId);
                    table.ForeignKey(
                        name: "FK_Commandes_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "PersonneId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commandes_LotCommandes_LotCommandeId",
                        column: x => x.LotCommandeId,
                        principalTable: "LotCommandes",
                        principalColumn: "LotCommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commandes_Medicaments_MedicamentId",
                        column: x => x.MedicamentId,
                        principalTable: "Medicaments",
                        principalColumn: "MedicamentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeLivraison",
                columns: table => new
                {
                    CommandesCommandeId = table.Column<int>(type: "int", nullable: false),
                    LivraisonsLivraisonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeLivraison", x => new { x.CommandesCommandeId, x.LivraisonsLivraisonId });
                    table.ForeignKey(
                        name: "FK_CommandeLivraison_Commandes_CommandesCommandeId",
                        column: x => x.CommandesCommandeId,
                        principalTable: "Commandes",
                        principalColumn: "CommandeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommandeLivraison_Livraisons_LivraisonsLivraisonId",
                        column: x => x.LivraisonsLivraisonId,
                        principalTable: "Livraisons",
                        principalColumn: "LivraisonId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_categoryMedicaments_CategoryMedicamentId",
                table: "categoryMedicaments",
                column: "CategoryMedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_WhishlistId",
                table: "Clients",
                column: "WhishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeLivraison_LivraisonsLivraisonId",
                table: "CommandeLivraison",
                column: "LivraisonsLivraisonId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_ClientId",
                table: "Commandes",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_LotCommandeId",
                table: "Commandes",
                column: "LotCommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Commandes_MedicamentId",
                table: "Commandes",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_CommentaireParentId",
                table: "Commentaire",
                column: "CommentaireParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_MedicamentId",
                table: "Commentaire",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Commentaire_PersonneId",
                table: "Commentaire",
                column: "PersonneId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_CommandeId",
                table: "Factures",
                column: "CommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_FournisseurId",
                table: "Lots",
                column: "FournisseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_LotCommandeId",
                table: "Lots",
                column: "LotCommandeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lots_MedicamentId",
                table: "Lots",
                column: "MedicamentId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicamentWhishlist_WhishlistsWhishlistId",
                table: "MedicamentWhishlist",
                column: "WhishlistsWhishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_permissions_PasseportId",
                table: "permissions",
                column: "PasseportId");

            migrationBuilder.CreateIndex(
                name: "IX_Personnes_PasseportId",
                table: "Personnes",
                column: "PasseportId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminCommandes");

            migrationBuilder.DropTable(
                name: "AdminPrincipals");

            migrationBuilder.DropTable(
                name: "AdminProduits");

            migrationBuilder.DropTable(
                name: "AdminStocks");

            migrationBuilder.DropTable(
                name: "categoryMedicaments");

            migrationBuilder.DropTable(
                name: "CommandeLivraison");

            migrationBuilder.DropTable(
                name: "Commentaire");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "MedicamentWhishlist");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Livraisons");

            migrationBuilder.DropTable(
                name: "Commandes");

            migrationBuilder.DropTable(
                name: "Fournisseurs");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "LotCommandes");

            migrationBuilder.DropTable(
                name: "Medicaments");

            migrationBuilder.DropTable(
                name: "Personnes");

            migrationBuilder.DropTable(
                name: "Whishlists");

            migrationBuilder.DropTable(
                name: "Passeports");
        }
    }
}
