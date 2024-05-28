using System.ComponentModel.DataAnnotations;

namespace SophaTemp.Viewmodel
{
    public class ClientVm
    {
        public int ClientId { get; set; }   
        public string LibellePharmacie { get; set; }
        public string Ville { get; set; }
        public string Telephone { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string Adresse { get; set; }
        public bool EnGarde { get; set; }

        public string nom { get; set; }
        public string prenom { get; set; }

        [Required]
        [EmailAddress]

        public string email { get; set; }
        [DataType(DataType.Password)]
        public string motdepasse { get; set; }

        public int WhishlistId { get; set; }



    }
}
