namespace SophaTemp.Models
{
    public class Fournisseur
    {
        public int FournisseurId { get; set; }
        public string NomComplet { get; set; }


        public ICollection<Lot> Lots { get; set; }

        public Fournisseur()
        {
            Lots = new HashSet<Lot>();
        }
    }
}
