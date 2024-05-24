namespace SophaTemp.Models
{
    public class LotCommande
    {
        public int LotCommandeId { get; set; }

        public double Frais { get; set; }
        public int Quantite { get; set; }
        public ICollection<Lot> Lots { get; set; }

        public ICollection<Commande> commandes { get; set; }

    }
}
