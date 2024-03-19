namespace SophaTemp.Models
{
    public class Client : Personne
    {
        public string LibellePharmacie { get; set; }
        public string Ville { get; set; }


        public virtual ICollection<Commande> Commandes { get; set; }
        public int WhishlistId { get; set; }
        public Whishlist Whishlist { get; set; }
    }
}
