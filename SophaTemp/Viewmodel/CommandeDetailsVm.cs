namespace SophaTemp.Viewmodel
{
    public class CommandeDetailsVm
    {
        public int CommandeId { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }
        public int Quantite { get; set; }
        public string ClientNom { get; set; }
        public string MedicamentNom { get; set; }
    }
}
