namespace SophaTemp.Models
{
    public class Livraison
    {
        public int LivraisonId { get; set; }
        public DateTime DateLivraison { get; set; }

        public virtual ICollection<Commande> Commandes { get; set; }
    }
}
