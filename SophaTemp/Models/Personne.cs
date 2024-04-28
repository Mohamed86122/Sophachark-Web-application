using System.ComponentModel.DataAnnotations;

namespace SophaTemp.Models
{
    public class Personne
    {
        [Key]
        public int PersonneId { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }

        public string email { get; set; }

        public string motdepasse { get; set; }

        public Passeport Passeport { get; set; }

     
        public int PasseportId { get; set; }

    }
}
