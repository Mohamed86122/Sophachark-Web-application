namespace SophaTemp.Models
{
    public class Commande
    {
        public int CommandeId { get; set; }
        public string Numero { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }

        // Propriétés de navigation
        public virtual ICollection<LotCommande> LotCommandes { get; set; }
        public virtual ICollection<Livraison> Livraisons { get; set; }
    }
}
