namespace SophaTemp.Models
{
    public class DetailMedicament
    {
        public Medicament Medicament { get; set; }
        public Lot Lot { get; set; }
        public List<Medicament> RelatedProducts { get; set; }
    }
}
