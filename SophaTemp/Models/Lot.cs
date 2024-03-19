namespace SophaTemp.Models
{
    public class Lot
    {
        public int LotId { get; set; }
        public double Montant { get; set; }
        public int Quantite { get; set; }
        public int PrixAchat { get; set; }
        public int PrixVente { get; set; }
        public DateTime DateDeProduction { get; set; }
        public DateTime DateDExpedition { get; set; }

        // Relations

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }
        public int FournisseurId { get; set; }
        public Fournisseur Fournisseur { get; set; }
    }
}
