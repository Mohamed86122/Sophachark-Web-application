namespace SophaTemp.Models
{
    public class Commande
    {
        internal object lotCommande;

        public int CommandeId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        
        public DateTime DateCommande { get; set; }
        public string Status { get; set; } = "En cours";

        public int Quantite {  get; set; }
        public ICollection<LotCommande> LotsCommande { get; set; }

        public int MedicamentId { get; set; } 
        public Medicament Medicament { get; set; }

        public virtual ICollection<Livraison> Livraisons { get; set; }


    }
}
