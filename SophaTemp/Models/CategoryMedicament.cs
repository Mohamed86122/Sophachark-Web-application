namespace SophaTemp.Models
{
    public class CategoryMedicament
    {
        public int CategoryMedicamentId { get; set; }
        public string Reference { get; set; }
        public string Libelle { get; set; }

        // Propriété de navigation
        public ICollection<MedicamentCategoryMedicament> MedicamentCategoryMedicaments { get; set; }


    }
}
