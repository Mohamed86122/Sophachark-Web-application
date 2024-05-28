namespace SophaTemp.Models
{
    public class Client : Personne
    {
        public int ClientId { get; set; }
        public string LibellePharmacie { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Adresse { get; set; }
        public bool EnGarde { get; set; }

        
        public virtual ICollection<Commande> Commandes { get; set; }
        public int WhishlistId { get; set; }
        public Whishlist Whishlist { get; set; }
        public int PasseportId { get; set; }
        public Passeport Passeport { get; set; }

        // Clé étrangère vers Personne
        public int PersonneId { get; set; }
        public Personne Personne { get; set; }
    }
}
