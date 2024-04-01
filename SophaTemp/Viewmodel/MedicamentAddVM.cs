using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class MedicamentAddVM
    {
        public string Reference { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public int QuantiteEnAlerte { get; set; }
        public List<int>? SelectedCategorieIds { get; set; }

        public MedicamentAddVM () 
        {
            SelectedCategorieIds = new List<int> ();
        }   

    }
}
