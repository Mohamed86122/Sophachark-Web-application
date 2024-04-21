using System.ComponentModel.DataAnnotations.Schema;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SophaTemp.Models
{
    public class Passeport
    {
        public int PasseportId { get; set; }
        public string Nom { get; set; }
		public ICollection<Permission> Permissions { get; set; }

		public Personne Personne { get; set; }

    }
}
