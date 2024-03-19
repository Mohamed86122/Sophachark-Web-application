using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SophaTemp.Models
{
    public class Passeport
    {
        public int PasseportId { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string PermissionsJson { get; set; }

        [NotMapped]
        public List<Dictionary<string, string>>? Permissions
        {
            get => string.IsNullOrEmpty(PermissionsJson)
                   ? new List<Dictionary<string, string>>()
                   : JsonSerializer.Deserialize<List<Dictionary<string, string>>>(PermissionsJson);
            set => PermissionsJson = JsonSerializer.Serialize(value);
        }
        public Personne Personne { get; set; }

    }
}
