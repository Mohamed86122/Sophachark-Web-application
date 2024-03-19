namespace SophaTemp.Models
{
    public class Commentaire
    {
        public int CommentaireId { get; set; }
        public string Numero { get; set; }
        public DateTime Date { get; set; }
        public string Contenu { get; set; }

        public int PersonneId { get; set; }
        public Personne Personne { get; set; }

        public int MedicamentId { get; set; }
        public Medicament Medicament { get; set; }

        public int? CommentaireParentId { get; set; }
        public Commentaire CommentaireParent { get; set; }
        public ICollection<Commentaire> Reponses { get; set; }

        public Commentaire()
        {
            Reponses = new HashSet<Commentaire>();
        }
    }
}
