using SophaTemp.Models;
using System.Collections.Generic;

namespace SophaTemp.ViewModels
{
    public class CategoryMedicationsViewModel
    {
        public CategoryMedicament Category { get; set; }
        public List<Medicament> Medications { get; set; }
        public List<CategoryMedicament> Categories { get; set; }
    }
}
