using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class CommandeVm
    {
        public int ClientId { get; set; }
        public int MedicamentId { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }
        public int Quantite { get; set; }
        public List<LotSelection> LotSelections { get; set; }
        public ICollection<Livraison>? Livraisons { get; set; }

        // Ajout de la propriété pour stocker les données des lots sélectionnés au format JSON
        public string SelectedLotsJson { get; set; }
    }
}
