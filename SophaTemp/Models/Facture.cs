namespace SophaTemp.Models
{
    public class Facture
    {
        public int FactureId { get; set; }
        public string Numero { get; set; }
        public double Montant { get; set; }
        public DateTime DateFacturation { get; set; }

        public ICollection<Commande> Commandes { get; set; }

        public Facture()
        {
            Commandes = new HashSet<Commande>();
        }

        public void GenerationFacture()
        {

        }

    }
}
