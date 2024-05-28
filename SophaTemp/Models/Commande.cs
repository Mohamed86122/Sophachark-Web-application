namespace SophaTemp.Models
{
    public class Commande
    {

        public int CommandeId { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        
        public DateTime DateCommande { get; set; }
        public string Status { get; set; } = "En cours";

        public int Quantite {  get; set; }
        
        public int LotCommandeId { get; set; }

        public LotCommande LotCommande { get; set; }
        public int MedicamentId { get; set; } 
        public Medicament Medicament { get; set; }

        public virtual ICollection<Facture> Factures { get; set; }

       
        public virtual ICollection<Livraison> Livraisons { get; set; }


    }
}
