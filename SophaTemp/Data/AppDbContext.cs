using Microsoft.EntityFrameworkCore;
using SophaTemp.Models;

namespace SophaTemp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        public AppDbContext() { }
        public DbSet<Personne> Personnes { get; set; }
        public DbSet<Passeport> Passeports { get; set; }
        public DbSet<AdminPrincipal> AdminPrincipals { get; set; }
        public DbSet<AdminProduit> AdminProduits { get; set; }
        public DbSet<AdminStock> AdminStocks { get; set; }
        public DbSet<AdminCommande> AdminCommandes { get; set; }
        
        public DbSet<Commande> Commandes { get; set; }
        public DbSet<Livraison> Livraisons { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Whishlist> Whishlists { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<LotCommande> LotCommandes { get; set; }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Facture> Factures { get; set; }

        public DbSet<CategoryMedicament> Categories { get; set; } 

        public DbSet<MedicamentCategoryMedicament> categoryMedicaments { get; set; }

        public DbSet<Permission> permissions { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MedicamentCategoryMedicament>()
                .HasKey(mcm => new { mcm.MedicamentId, mcm.CategoryMedicamentId });

            modelBuilder.Entity<MedicamentCategoryMedicament>()
                .HasOne(mcm => mcm.Medicament)
                .WithMany(m => m.MedicamentCategoryMedicaments)
                .HasForeignKey(mcm => mcm.MedicamentId);

            modelBuilder.Entity<MedicamentCategoryMedicament>()
                .HasOne(mcm => mcm.CategoryMedicament)
                .WithMany(cm => cm.MedicamentCategoryMedicaments)
                .HasForeignKey(mcm => mcm.CategoryMedicamentId);


            modelBuilder.Entity<Personne>().HasKey(p => p.PersonneId);

            

        }











    }
}
