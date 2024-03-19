namespace SophaTemp.Models
{
    public class Whishlist
    {
        public int WhishlistId { get; set; }
        public int X { get; set; } // À déterminer selon l'usage
        public int Y { get; set; } // À déterminer selon l'usage

        // Propriété de navigation
        public virtual ICollection<Medicament> Medicaments { get; set; }
    }
}
