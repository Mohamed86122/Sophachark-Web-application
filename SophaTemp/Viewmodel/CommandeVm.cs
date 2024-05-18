﻿using SophaTemp.Models;

namespace SophaTemp.Viewmodel
{
    public class CommandeVm
    {
        public int ClientId { get; set; }
        public int MedicamentId { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }
        public int Quantite { get; set; }
        public List<LotSelection> LotSelections { get; set; }
        public ICollection<Livraison>? Livraisons { get; set; }

        public string SelectedLotsString { get; set; }
    }

    public class LotSelection
    {
        public int LotId { get; set; }
        public int Quantite { get; set; }
        public int MedicamentId { get; set; }
    }


}
