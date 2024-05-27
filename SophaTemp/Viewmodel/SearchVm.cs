using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class SearchVm
    {
        public int MedicamentId { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float PrixVente { get; set; }
        public int Quantite { get; set; }
    }
}
