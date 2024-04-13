namespace SophaTemp.Viewmodel
{
    public class CommandeVm
    {
        public int ClientId { get; set; }
        public int MedicamentId { get; set; }
        public DateTime DateCommande { get; set; }
        public string Status { get; set; }
        public IEnumerable<int> SelectedLotIds { get; set; } 
    }
}
