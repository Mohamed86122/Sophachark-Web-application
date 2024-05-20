namespace SophaTemp.Models
{
    public class Lot
    {
        public int LotId { get; set; }
        public double Montant { get; set; }

        public string Libelle { get; set; }
        public int Quantite { get; set; }
        public float PrixAchat { get; set; }
        public float PrixVente { get; set; }
        public DateTime DateDeProduction { get; set; }
        public DateTime DateDExpedition { get; set; }

        // Relations

        public int MedicamentId { get; set; }

        public Boolean IsPublic { get; set; }
        public Medicament Medicament { get; set; }
        public int FournisseurId { get; set; }
        public Fournisseur Fournisseur { get; set; }
    }
}
