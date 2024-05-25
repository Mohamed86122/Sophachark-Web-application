using SophaTemp.Models;
using System.ComponentModel.DataAnnotations;

namespace SophaTemp.Viewmodel
{
    public class PerViewModel
    {
        public string nom { get; set; }
        public string prenom { get; set; }

        public string email { get; set; }


        [DataType(DataType.Password)]
        public string motdepasse { get; set; }

        public int PasseportId { get; set; }


    }
}
