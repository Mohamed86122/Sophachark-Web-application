﻿    using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class CommandeVm
    {
        public int ClientId { get; set; }
        public int MedicamentId { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }
        public int Quantite { get; set; } 
        public int IdLotCommande { get; set; }
        public List<LotSelection> LotSelections { get; set; } = new List<LotSelection>();
        public ICollection<Livraison> Livraisons { get; set; }  
    }
}
