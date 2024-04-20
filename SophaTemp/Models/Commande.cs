namespace SophaTemp.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public string Numero { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }

        public string Quantite {  get; set; }
        public int IdLotCommande { get; set; }
        public LotCommande lotCommande { get; set; }
        public virtual ICollection<Livraison> Livraisons { get; set; }
    }
}
