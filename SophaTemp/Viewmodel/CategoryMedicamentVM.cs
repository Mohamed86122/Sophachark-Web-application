using System.ComponentModel.DataAnnotations;

namespace SophaTemp.Viewmodel
{
    public class CategoryMedicamentVM
    {

        [Required(ErrorMessage = "La référence est obligatoire.")]
        [Display(Name = "Référence")]
        public string Reference { get; set; }

        [Required(ErrorMessage = "Le libellé est obligatoire.")]
        [Display(Name = "Libellé")]
        public string Libelle { get; set; }
    }
}
