using System.ComponentModel.DataAnnotations;

namespace SophaTemp.Viewmodel
{
    public class LotCommandeVm
    {
        [Required]
        public int LotCommandeId { get; set; }
        [Required]
        public double Frais { get; set; }
        [Required]
        public int Quantite { get; set; }
        [Required]
        public List<int> LotId { get; set; }

    }
}
