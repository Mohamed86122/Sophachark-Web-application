using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class CommandeVm
    {
        public int ClientId { get; set; }
        public int MedicamentId { get; set; } = 0;
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }
        public int Quantite { get; set; }
        public List<Lot> Lots { get; set; } = new List<Lot>(); // Utilisation de la liste de Lot directement
        public int IdLotCommande { get; set; }
        public LotCommande LotCommande { get; set; }
        public ICollection<Livraison> Livraisons { get; set; }  
    }
}
