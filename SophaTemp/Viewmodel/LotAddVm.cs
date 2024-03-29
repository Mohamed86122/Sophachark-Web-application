using SophaTemp.Models;
using System.ComponentModel.DataAnnotations;

namespace SophaTemp.Viewmodel
{
    public class LotAddVm
    {
        [Required]
        public float Montant { get; set; }
        [Required]
        public int Quantite { get; set; }
        [Required]
        public int PrixAchat { get; set; }
        [Required]
        public int PrixVente { get; set; }
        [Required]
        public DateTime DateDeProduction { get; set; }
        [Required]
        public DateTime DateDExpedition { get; set; }

        public int MedicamentId { get; set; }
        public int FournisseurId { get; set; }

    }
}
