namespace SophaTemp.Models
{
    public class Medicament
    {
        public int MedicamentId { get; set; }
        public string Reference { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int QuantiteEnAlerte { get; set; }
        //public int Quantite { get; set; }

        // Propriétés de navigation
        public virtual ICollection<Whishlist> Whishlists { get; set; }
        public virtual ICollection<Commentaire> Commentaires { get; set; }
        public ICollection<Commande> Commandes { get; set; }
        public ICollection<MedicamentCategoryMedicament> MedicamentCategoryMedicaments { get; set; }


    }
}
