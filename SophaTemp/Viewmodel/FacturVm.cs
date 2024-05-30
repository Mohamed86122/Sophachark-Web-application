namespace SophaTemp.Viewmodel
{
    public class FacturVm
    {
        public string ClientNom { get; set; }
        public DateTime DateCommande { get; set; }
        public List<CommandeDetailsVm> Details { get; set; }
    }
}
