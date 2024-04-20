using System.ComponentModel.DataAnnotations.Schema;

namespace SophaTemp.Models
{
    public class Facture
    {
        public int FactureId { get; set; }
        public string Numero { get; set; }
        public double Montant { get; set; }
        public DateTime DateFacturation { get; set; }

        public int CommandeId { get; set; }

        [ForeignKey("CommandeId")]
        public Commande Commande { get; set; }

        

        public void GenerationFacture()
        {

        }

    }
}
