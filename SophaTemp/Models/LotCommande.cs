namespace SophaTemp.Models
{
    public class LotCommande
    {
        public int LotCommandeId { get; set; }
        public double Frais { get; set; }
        public int Quantite { get; set; }

        public int LotId { get; set; }
        public Lot Lot { get; set; }

    }
}
