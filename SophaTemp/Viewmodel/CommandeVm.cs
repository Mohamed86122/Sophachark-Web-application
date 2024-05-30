using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class CommandeVm
    {
        public int ClientId { get; set; }
        public int MedicamentId { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }
        public int LotCommandeId { get; set; }
        public String Data { get; set; }
        public ICollection<Livraison>? Livraisons { get; set; }

        
    }

}
