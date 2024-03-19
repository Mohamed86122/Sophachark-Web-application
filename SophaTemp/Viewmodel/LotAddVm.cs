using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class LotAddVm
    {
        public double Montant { get; set; }
        public int Quantite { get; set; }
        public int PrixAchat { get; set; }
        public int PrixVente { get; set; }
        public DateTime DateDeProduction { get; set; }
        public DateTime DateDExpedition { get; set; }

        public int MedicamentId { get; set; }
        public int FournisseurId { get; set; }

    }
}
