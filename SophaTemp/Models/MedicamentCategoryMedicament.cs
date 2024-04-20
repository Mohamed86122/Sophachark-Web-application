using System.ComponentModel.DataAnnotations.Schema;

namespace SophaTemp.Models
{
    public class MedicamentCategoryMedicament
    {
        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int CategoryMedicamentId { get; set; }
        public CategoryMedicament CategoryMedicament { get; set; }


    }
}
