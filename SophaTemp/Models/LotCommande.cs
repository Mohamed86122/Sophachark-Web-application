namespace SophaTemp.Models
{
    public class LotCommande
    {
        public int LotCommandeId { get; set; }

        public double Frais { get; set; }
        public int Quantite { get; set; }

        public int CommandeId { get; set; }
        public Commande Commande { get; set; }
        public ICollection<Lot> Lots { get; set; }

    }
}
