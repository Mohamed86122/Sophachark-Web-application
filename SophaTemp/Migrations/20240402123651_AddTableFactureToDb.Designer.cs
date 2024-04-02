﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SophaTemp.Data;

#nullable disable

namespace SophaTemp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240402123651_AddTableFactureToDb")]
    partial class AddTableFactureToDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CategoryMedicamentMedicament", b =>
                {
                    b.Property<int>("CategoriesCategoryMedicamentId")
                        .HasColumnType("int");

                    b.Property<int>("MedicamentsMedicamentId")
                        .HasColumnType("int");

                    b.HasKey("CategoriesCategoryMedicamentId", "MedicamentsMedicamentId");

                    b.HasIndex("MedicamentsMedicamentId");

                    b.ToTable("CategoryMedicamentMedicament");
                });

            modelBuilder.Entity("CommandeLivraison", b =>
                {
                    b.Property<int>("CommandesCommandeId")
                        .HasColumnType("int");

                    b.Property<int>("LivraisonsLivraisonId")
                        .HasColumnType("int");

                    b.HasKey("CommandesCommandeId", "LivraisonsLivraisonId");

                    b.HasIndex("LivraisonsLivraisonId");

                    b.ToTable("CommandeLivraison");
                });

            modelBuilder.Entity("MedicamentWhishlist", b =>
                {
                    b.Property<int>("MedicamentsMedicamentId")
                        .HasColumnType("int");

                    b.Property<int>("WhishlistsWhishlistId")
                        .HasColumnType("int");

                    b.HasKey("MedicamentsMedicamentId", "WhishlistsWhishlistId");

                    b.HasIndex("WhishlistsWhishlistId");

                    b.ToTable("MedicamentWhishlist");
                });

            modelBuilder.Entity("SophaTemp.Models.CategoryMedicament", b =>
                {
                    b.Property<int>("CategoryMedicamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryMedicamentId"), 1L, 1);

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryMedicamentId");

                    b.ToTable("CategoryMedicament");
                });

            modelBuilder.Entity("SophaTemp.Models.Commande", b =>
                {
                    b.Property<int>("CommandeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommandeId"), 1L, 1);

                    b.Property<int?>("ClientPersonneId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCommande")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdLotCommande")
                        .HasColumnType("int");

                    b.Property<int>("LotCommandeId")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CommandeId");

                    b.HasIndex("ClientPersonneId");

                    b.HasIndex("LotCommandeId");

                    b.ToTable("Commandes");
                });

            modelBuilder.Entity("SophaTemp.Models.Commentaire", b =>
                {
                    b.Property<int>("CommentaireId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentaireId"), 1L, 1);

                    b.Property<int?>("CommentaireParentId")
                        .HasColumnType("int");

                    b.Property<string>("Contenu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedicamentId")
                        .HasColumnType("int");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonneId")
                        .HasColumnType("int");

                    b.HasKey("CommentaireId");

                    b.HasIndex("CommentaireParentId");

                    b.HasIndex("MedicamentId");

                    b.HasIndex("PersonneId");

                    b.ToTable("Commentaire");
                });

            modelBuilder.Entity("SophaTemp.Models.Facture", b =>
                {
                    b.Property<int>("FactureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FactureId"), 1L, 1);

                    b.Property<int>("CommandeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateFacturation")
                        .HasColumnType("datetime2");

                    b.Property<double>("Montant")
                        .HasColumnType("float");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FactureId");

                    b.HasIndex("CommandeId");

                    b.ToTable("Factures");
                });

            modelBuilder.Entity("SophaTemp.Models.Fournisseur", b =>
                {
                    b.Property<int>("FournisseurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FournisseurId"), 1L, 1);

                    b.Property<string>("NomComplet")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FournisseurId");

                    b.ToTable("Fournisseurs");
                });

            modelBuilder.Entity("SophaTemp.Models.Livraison", b =>
                {
                    b.Property<int>("LivraisonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LivraisonId"), 1L, 1);

                    b.Property<DateTime>("DateLivraison")
                        .HasColumnType("datetime2");

                    b.HasKey("LivraisonId");

                    b.ToTable("Livraisons");
                });

            modelBuilder.Entity("SophaTemp.Models.Lot", b =>
                {
                    b.Property<int>("LotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LotId"), 1L, 1);

                    b.Property<DateTime>("DateDExpedition")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateDeProduction")
                        .HasColumnType("datetime2");

                    b.Property<int>("FournisseurId")
                        .HasColumnType("int");

                    b.Property<int>("MedicamentId")
                        .HasColumnType("int");

                    b.Property<double>("Montant")
                        .HasColumnType("float");

                    b.Property<int>("PrixAchat")
                        .HasColumnType("int");

                    b.Property<int>("PrixVente")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("LotId");

                    b.HasIndex("FournisseurId");

                    b.HasIndex("MedicamentId");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("SophaTemp.Models.LotCommande", b =>
                {
                    b.Property<int>("LotCommandeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LotCommandeId"), 1L, 1);

                    b.Property<double>("Frais")
                        .HasColumnType("float");

                    b.Property<int>("LotId")
                        .HasColumnType("int");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.HasKey("LotCommandeId");

                    b.HasIndex("LotId");

                    b.ToTable("LotCommandes");
                });

            modelBuilder.Entity("SophaTemp.Models.Medicament", b =>
                {
                    b.Property<int>("MedicamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedicamentId"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantiteEnAlerte")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MedicamentId");

                    b.ToTable("Medicaments");
                });

            modelBuilder.Entity("SophaTemp.Models.MedicamentCategoryMedicament", b =>
                {
                    b.Property<int>("MedicamentId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryMedicamentId")
                        .HasColumnType("int");

                    b.HasKey("MedicamentId", "CategoryMedicamentId");

                    b.HasIndex("CategoryMedicamentId");

                    b.ToTable("categoryMedicaments");
                });

            modelBuilder.Entity("SophaTemp.Models.Passeport", b =>
                {
                    b.Property<int>("PasseportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PasseportId"), 1L, 1);

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PermissionsJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PasseportId");

                    b.ToTable("Passeports");
                });

            modelBuilder.Entity("SophaTemp.Models.Personne", b =>
                {
                    b.Property<int>("PersonneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonneId"), 1L, 1);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PasseportId")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("motdepasse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonneId");

                    b.HasIndex("PasseportId")
                        .IsUnique();

                    b.ToTable("Personnes");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Personne");
                });

            modelBuilder.Entity("SophaTemp.Models.Whishlist", b =>
                {
                    b.Property<int>("WhishlistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WhishlistId"), 1L, 1);

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("WhishlistId");

                    b.ToTable("Whishlists");
                });

            modelBuilder.Entity("SophaTemp.Models.AdminCommande", b =>
                {
                    b.HasBaseType("SophaTemp.Models.Personne");

                    b.HasDiscriminator().HasValue("AdminCommande");
                });

            modelBuilder.Entity("SophaTemp.Models.AdminPrincipal", b =>
                {
                    b.HasBaseType("SophaTemp.Models.Personne");

                    b.HasDiscriminator().HasValue("AdminPrincipal");
                });

            modelBuilder.Entity("SophaTemp.Models.AdminProduit", b =>
                {
                    b.HasBaseType("SophaTemp.Models.Personne");

                    b.HasDiscriminator().HasValue("AdminProduit");
                });

            modelBuilder.Entity("SophaTemp.Models.AdminStock", b =>
                {
                    b.HasBaseType("SophaTemp.Models.Personne");

                    b.HasDiscriminator().HasValue("AdminStock");
                });

            modelBuilder.Entity("SophaTemp.Models.Client", b =>
                {
                    b.HasBaseType("SophaTemp.Models.Personne");

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EnGarde")
                        .HasColumnType("bit");

                    b.Property<string>("LibellePharmacie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telephone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WhishlistId")
                        .HasColumnType("int");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.HasIndex("WhishlistId");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("CategoryMedicamentMedicament", b =>
                {
                    b.HasOne("SophaTemp.Models.CategoryMedicament", null)
                        .WithMany()
                        .HasForeignKey("CategoriesCategoryMedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SophaTemp.Models.Medicament", null)
                        .WithMany()
                        .HasForeignKey("MedicamentsMedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CommandeLivraison", b =>
                {
                    b.HasOne("SophaTemp.Models.Commande", null)
                        .WithMany()
                        .HasForeignKey("CommandesCommandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SophaTemp.Models.Livraison", null)
                        .WithMany()
                        .HasForeignKey("LivraisonsLivraisonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedicamentWhishlist", b =>
                {
                    b.HasOne("SophaTemp.Models.Medicament", null)
                        .WithMany()
                        .HasForeignKey("MedicamentsMedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SophaTemp.Models.Whishlist", null)
                        .WithMany()
                        .HasForeignKey("WhishlistsWhishlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SophaTemp.Models.Commande", b =>
                {
                    b.HasOne("SophaTemp.Models.Client", null)
                        .WithMany("Commandes")
                        .HasForeignKey("ClientPersonneId");

                    b.HasOne("SophaTemp.Models.LotCommande", "lotCommande")
                        .WithMany()
                        .HasForeignKey("LotCommandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("lotCommande");
                });

            modelBuilder.Entity("SophaTemp.Models.Commentaire", b =>
                {
                    b.HasOne("SophaTemp.Models.Commentaire", "CommentaireParent")
                        .WithMany("Reponses")
                        .HasForeignKey("CommentaireParentId");

                    b.HasOne("SophaTemp.Models.Medicament", "Medicament")
                        .WithMany("Commentaires")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SophaTemp.Models.Personne", "Personne")
                        .WithMany()
                        .HasForeignKey("PersonneId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommentaireParent");

                    b.Navigation("Medicament");

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("SophaTemp.Models.Facture", b =>
                {
                    b.HasOne("SophaTemp.Models.Commande", "Commande")
                        .WithMany()
                        .HasForeignKey("CommandeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Commande");
                });

            modelBuilder.Entity("SophaTemp.Models.Lot", b =>
                {
                    b.HasOne("SophaTemp.Models.Fournisseur", "Fournisseur")
                        .WithMany("Lots")
                        .HasForeignKey("FournisseurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SophaTemp.Models.Medicament", "Medicament")
                        .WithMany()
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fournisseur");

                    b.Navigation("Medicament");
                });

            modelBuilder.Entity("SophaTemp.Models.LotCommande", b =>
                {
                    b.HasOne("SophaTemp.Models.Lot", "Lot")
                        .WithMany()
                        .HasForeignKey("LotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lot");
                });

            modelBuilder.Entity("SophaTemp.Models.MedicamentCategoryMedicament", b =>
                {
                    b.HasOne("SophaTemp.Models.CategoryMedicament", "CategoryMedicament")
                        .WithMany("MedicamentCategoryMedicaments")
                        .HasForeignKey("CategoryMedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SophaTemp.Models.Medicament", "Medicament")
                        .WithMany("MedicamentCategoryMedicaments")
                        .HasForeignKey("MedicamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryMedicament");

                    b.Navigation("Medicament");
                });

            modelBuilder.Entity("SophaTemp.Models.Personne", b =>
                {
                    b.HasOne("SophaTemp.Models.Passeport", "Passeport")
                        .WithOne("Personne")
                        .HasForeignKey("SophaTemp.Models.Personne", "PasseportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passeport");
                });

            modelBuilder.Entity("SophaTemp.Models.Client", b =>
                {
                    b.HasOne("SophaTemp.Models.Whishlist", "Whishlist")
                        .WithMany("Clients")
                        .HasForeignKey("WhishlistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Whishlist");
                });

            modelBuilder.Entity("SophaTemp.Models.CategoryMedicament", b =>
                {
                    b.Navigation("MedicamentCategoryMedicaments");
                });

            modelBuilder.Entity("SophaTemp.Models.Commentaire", b =>
                {
                    b.Navigation("Reponses");
                });

            modelBuilder.Entity("SophaTemp.Models.Fournisseur", b =>
                {
                    b.Navigation("Lots");
                });

            modelBuilder.Entity("SophaTemp.Models.Medicament", b =>
                {
                    b.Navigation("Commentaires");

                    b.Navigation("MedicamentCategoryMedicaments");
                });

            modelBuilder.Entity("SophaTemp.Models.Passeport", b =>
                {
                    b.Navigation("Personne")
                        .IsRequired();
                });

            modelBuilder.Entity("SophaTemp.Models.Whishlist", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("SophaTemp.Models.Client", b =>
                {
                    b.Navigation("Commandes");
                });
#pragma warning restore 612, 618
        }
    }
}
